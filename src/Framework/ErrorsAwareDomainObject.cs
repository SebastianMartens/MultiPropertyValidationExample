//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;


namespace MultiPropertyValidationExample.Framework
{
    /// <summary>
    /// Base domain object class.
    /// </summary>
    /// <remarks>
    /// Provides support for implementing <see cref="INotifyPropertyChanged"/> and 
    /// implements <see cref="INotifyDataErrorInfo"/> using <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> instances
    /// on the validated properties.    
    /// 
    /// INotifyDataErrorInfo: HasErrors/GetErrors/ErrorsChanged; only available > .Net 4.5!! But you can use most other classes as "ValidationResult"...
    /// IDataErrorInfo: Error/Item
    /// </remarks>
    public abstract class ErrorsAwareDomainObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {         
        #region ability to store errors in ErrorsContainer and INotifyDataErrorInfo (HasErrors/GetErrors/ErrorsChanged)

        private ErrorsContainer<ValidationResult> _errorsContainer;

        /// <summary>
        /// Event raised when the validation status changes.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the error status.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        /// <summary>
        /// Gets the container for errors in the properties of the domain object.
        /// </summary>        
        protected ErrorsContainer<ValidationResult> ErrorsContainer
        {
            get {                
                return _errorsContainer ??
                       (_errorsContainer = new ErrorsContainer<ValidationResult>(RaiseErrorsChanged));
            }
        }
       
        /// <summary>
        /// Returns the errors for <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the propertyName for which the errors are requested.</param>
        /// <returns>An enumerable with the errors.</returns>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsContainer.GetErrors(propertyName);
        }

        #endregion


        /// <summary>
        /// TODO: this is only my temporary helper.
        /// => Find more elegant solution to bind to errors (we need a simple property with getter instead of a get-method)
        /// E.g. bind to "ErrorsChanged" event and so on...
        /// </summary>
        public string[] GlobalErrorsArray { get { return ErrorsContainer.GetErrors("").Select(e => e.ErrorMessage).ToArray(); } }


        #region property validation

        /// <summary>
        /// Validates <paramref name="value"/> as the value for the propertyName named <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the propertyName.</param>
        /// <param name="value">The value for the propertyName.</param>
        protected void ValidateProperty(string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");            

            ValidateProperty(new ValidationContext(this, null, null) { MemberName = propertyName }, value);
        }

        /// <summary>
        /// Validates <paramref name="value"/> as the value for the propertyName specified by 
        /// <paramref name="validationContext"/> using data annotations validation attributes.
        /// </summary>
        /// <param name="validationContext">The context for the validation.</param>
        /// <param name="value">The value for the propertyName.</param>
        protected virtual void ValidateProperty(ValidationContext validationContext, object value)
        {
            if (validationContext == null)
                throw new ArgumentNullException("validationContext");            

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);

            ErrorsContainer.SetErrors(validationContext.MemberName, validationResults);
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the propertyName which changed its error status.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Method supports event.")]
        protected void RaiseErrorsChanged(string propertyName)
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="e">The argument for the event.</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            var handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }          

        public void SetError(string propertyName, string errorMessage)
        {
            ErrorsContainer.SetErrors(propertyName, 
                new List<ValidationResult>
                    {
                        new ValidationResult(errorMessage)
                    });
            
            //RaiseErrorsChanged(propertyName); // ??
            RaisePropertyChanged("HasErrors");
            RaisePropertyChanged("GlobalErrorsArray");
        }

        public void SetError(string errorMessage)
        {
            SetError("", errorMessage);            
        }

        #endregion

        #region raiseproperty changed stuff (maybe part of some other framework base class...)

        /// <summary>
        /// Event raised when a propertyName value changes.
        /// </summary>
        /// <seealso cref="INotifyPropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the changed propertyName.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Method supports event.")]
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The argument for the event.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}