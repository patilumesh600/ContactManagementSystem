using ContactManagementSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactManagementSystem.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        CONTACT_MGMT_DBEntities dbObject = new CONTACT_MGMT_DBEntities();
        public ActionResult Person(Person_Contact personContact)
        {
            if (personContact != null && personContact.ID > 0)
                return View("UpdatePersonContact", personContact);
            else
                return View();
        }

        [HttpPost]
        public ActionResult AddPersonContact(Person_Contact personContact)
        {
            var contact = dbObject.Person_Contact.Where(x => x.PhoneNumber == personContact.PhoneNumber).FirstOrDefault();
            if (contact != null && contact.PhoneNumber == personContact.PhoneNumber)
            {
                ViewBag.ErrorMessage = "This Phone Number Is Associate With Another Contact";
                ModelState.AddModelError("PhoneNumber", "This Phone Number Is Associate With Another.");
                return View("Person", personContact);
            }
            else
            {
                Person_Contact personContactObj = new Person_Contact();
                if (ModelState.IsValid)
                {
                    personContactObj.ID = personContact.ID;
                    personContactObj.FirstName = personContact.FirstName;
                    personContactObj.MiddleName = personContact.MiddleName;
                    personContactObj.LastName = personContact.LastName;
                    personContactObj.Email = personContact.Email;
                    personContactObj.PhoneNumber = personContact.PhoneNumber;
                    personContactObj.Status = true;
                    personContactObj.IsDeleted = false;
                    personContactObj.CreatedDate = DateTime.Now;
                    personContactObj.ModifiedDate = DateTime.Now;

                    if (personContact.ID == 0)
                    {
                        dbObject.Person_Contact.Add(personContactObj);
                        dbObject.SaveChanges();
                    }
                    else
                    {
                        dbObject.Entry(personContactObj).State = System.Data.Entity.EntityState.Modified;
                        dbObject.SaveChanges();
                    }
                    ModelState.Clear();
                    var contactList = dbObject.Person_Contact.OrderByDescending(x => x.Status).ToList();
                    return View("PersonContactList", contactList);
                }

                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdatePersonContact(Person_Contact personContact)
        {
            var contact = dbObject.Person_Contact.Where(x => x.PhoneNumber == personContact.PhoneNumber & x.ID != personContact.ID).FirstOrDefault();
            if (contact != null && contact.PhoneNumber == personContact.PhoneNumber)
            {
                ViewBag.ErrorMessage = "This Phone Number Is Associate With Another Contact";
                ModelState.AddModelError("PhoneNumber", "This Phone Number Is Associate With Another.");
                return View("UpdatePersonContact", personContact);
            }
            else
            {
                Person_Contact personContactObj = new Person_Contact();
                if (ModelState.IsValid)
                {
                    personContactObj.ID = personContact.ID;
                    personContactObj.FirstName = personContact.FirstName;
                    personContactObj.MiddleName = personContact.MiddleName;
                    personContactObj.LastName = personContact.LastName;
                    personContactObj.Email = personContact.Email;
                    personContactObj.PhoneNumber = personContact.PhoneNumber;
                    personContactObj.Status = true;
                    personContactObj.IsDeleted = false;
                    personContactObj.CreatedDate = DateTime.Now;
                    personContactObj.ModifiedDate = DateTime.Now;

                    if (personContact.ID == 0)
                    {
                        dbObject.Person_Contact.Add(personContactObj);
                        dbObject.SaveChanges();
                    }
                    else
                    {
                        dbObject.Entry(personContactObj).State = System.Data.Entity.EntityState.Modified;
                        dbObject.SaveChanges();
                    }
                    ModelState.Clear();
                    var contactList = dbObject.Person_Contact.OrderByDescending(x => x.Status).ToList();
                    return View("PersonContactList", contactList);
                }
                return View();
            }
        }

        public ActionResult PersonContactList()
        {
            var contactList = dbObject.Person_Contact.OrderByDescending(x => x.Status).ToList();
            return View(contactList);
        }

        public ActionResult ActiveOrInActiveContact(int id)
        {
            var contact = dbObject.Person_Contact.Where(x => x.ID == id).FirstOrDefault();
            if (contact != null)
            {
                contact.Status = !contact.Status;
                dbObject.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                dbObject.SaveChanges();
            }
            var contactList = dbObject.Person_Contact.OrderByDescending(x => x.Status).ToList();
            return View("PersonContactList", contactList);
        }

        // This Method not in use
        public ActionResult ResetForm(Person_Contact personContact)
        {
            ModelState.Clear();
            return View();
        }

        // This Method not in use
        public ActionResult DeleteContact(int id)
        {
            var contact = dbObject.Person_Contact.Where(x => x.ID == id).FirstOrDefault();
            if (contact != null)
            {
                dbObject.Person_Contact.Remove(contact);
                dbObject.SaveChanges();
            }

            var contactList = dbObject.Person_Contact.OrderByDescending(x => x.Status).ToList();
            return View("PersonContactList", contactList);
        }
    }
}