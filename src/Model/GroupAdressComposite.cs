using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MultiPropertyValidationExample.Framework;

namespace MultiPropertyValidationExample.Model
{
    /// <summary>
    /// Composite of multile items and additional information.
    /// Used to binding and validation in a multi-propertyName example.
    /// </summary>
    /// TODO: check out custom validation attribute!
    //[CustomValidation(typeof(GroupAdressComposite), "Validate")]
    public class GroupAdressComposite : ErrorsAwareDomainObject, IValidatableObject
    {
        private GroupAdress _readAdress;
        public string Name { get; set; }

        public GroupAdress ReadAdress
        {
            get { return _readAdress; }
            set { 
                _readAdress = value;         
       
                // This does not work as GroupAdress is a value object an will never change!
                // TODO: solution??

                //ReadAdress.ErrorsChanged += delegate { ...}
                  
                var errors = Validate(new ValidationContext(this));
                foreach (var validationResult in errors)
                {
                    // TODO: show Row errors in UI!!
                    SetError(validationResult.ErrorMessage);
                }                            
                  
                
                //Validator.ValidateObject(this, new ValidationContext(this));
                  

            }
        }

        public GroupAdress WriteAdress { get; set; }

        public static GroupAdressComposite GetDummyItem(int seed)
        {
            var random = new Random(seed);
            return new GroupAdressComposite
                {
                    Name = String.Format("Adresse {0}", random.Next(0, 100)),
                    ReadAdress  = new GroupAdress((ushort) random.Next(0, 100)),
                    WriteAdress = new GroupAdress((ushort) random.Next(0, 100))
                };
        }

      

        /// <summary>
        /// Validation is usually implemented at domain-level.
        /// It can be quite complex if you would like to implement validation only once in a distributed client-server environment.
        /// But in this case we only have client-side validation.
        /// 
        /// Note: EF CodeFirst (> CTP5) will automatically call Validate() on "SaveChanges()"!
        /// ASP.NET MVC 3 will use IValidatableObject, too.
        /// 
        /// Example here: Validations like "one of two properties has to be filled" is part of the domain logic (not part of UI or ViewModels).
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // only return custom object-level validation errors here! This is going on automatically:
            // 1) Validate property-level attributes
            // 2) If any validators are invalid, abort validation returning the failure(s)
            // 3) Validate the object-level attributes
            // 4) If any validators are invalid, abort validation returning the failure(s)
            // 5) If on the desktop framework and the object implements IValidatableObject, then call its Validate method and return any failure(s)                                   
            
            // Associated objects must NOT be validated here, they are validating themselfes!
            //Validator.TryValidateProperty(ReadAdress, 
            //    new ValidationContext(this, null, null) { MemberName = "ReadAdress" }, 
            //    results);            
            
            if (
                (ReadAdress == null || ReadAdress.HasErrors) && 
                (WriteAdress == null || WriteAdress.HasErrors))
            {
                yield return new ValidationResult("At least one Group Adress has to be filled with a valid value.", new List<string>{"ReadAdress", "WriteAdress"});
            }            
        }
    }
}
