using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Useful.Static
{
    public class StaticList
    {
        public List<SelectListItem> listeRoles = new List<SelectListItem>() {
                new SelectListItem(){Text = "SuperAdmin",  Value ="SuperAdmin" },
                 new SelectListItem(){Text = "مدیر سایت",  Value ="Admin" },
                new SelectListItem(){Text = "مدیر گروه",  Value ="Maneger" },
                new SelectListItem(){Text =  "عضو گروه هم نوایی",  Value ="homophony" },
                 new SelectListItem(){Text = "تدوین گر",  Value ="Editor" },
                  new SelectListItem(){Text =  "فیلم بردار",  Value ="Cameraman" },
                new SelectListItem(){Text = "صدا بردار",  Value ="Sound_recordist" }

        };
        public List<KeyValuePair<string, string>> listeRolesEdited = new List<KeyValuePair<string, string>> {
               new KeyValuePair<string, string>( "SuperAdmin", "SuperAdmin" ),
                new KeyValuePair<string, string>("Admin",  "Admin" ),
               new KeyValuePair<string, string>("Maneger",  "مدیر گروه" ),
                new KeyValuePair<string, string>( "homophony",  "عضو گروه هم نوایی" ),
                new KeyValuePair<string, string>( "Editor", "تدوین گر" ),
               new KeyValuePair<string, string>("Cameraman",  "فیلم بردار" ),
                new KeyValuePair<string, string>("Sound_recordist", "صدا بردار" )
        };
        public List<KeyValuePair<string, string>> listeRolesEdited2 = new List<KeyValuePair<string, string>> {
               new KeyValuePair<string, string>(  "مدیر ارشد" , "SuperAdmin"),
                new KeyValuePair<string, string>(   "مدیر سایت" ,"Admin"),
               new KeyValuePair<string, string>( "مدیر گروه" , "Maneger" ),
                new KeyValuePair<string, string>(   "عضو گروه هم نوایی" ,"homophony"),
                new KeyValuePair<string, string>(  "تدوین گر" ,"Editor"),
               new KeyValuePair<string, string>(  "فیلم بردار" ,"Cameraman"),
                new KeyValuePair<string, string>( "صدا بردار" ,"Sound_recordist")
        };
        public static List<SelectListItem> NazarCategory = new List<SelectListItem>() {
            new SelectListItem() { Text = "تقدیر", Value ="1" },
             new SelectListItem() { Text = "پیشنهاد", Value ="2"  },
              new SelectListItem() { Text = "انتقاد", Value = "3"},
        };

        public List<SelectListItem> PlaceShow = new List<SelectListItem>() {
            new SelectListItem() { Text = "اسلایدر", Value ="1" },
             new SelectListItem() { Text = "محتوا", Value ="2"  },
              new SelectListItem() { Text = "پایین", Value = "3"},
        };

        public List<SelectListItem> FileType = new List<SelectListItem>() {
            new SelectListItem() { Text = "Image", Value ="0" },
            new SelectListItem() { Text = "Video", Value ="1" },
             new SelectListItem() { Text = "Music", Value ="2"  },

        };
        public List<KeyValuePair<string, string>> FileTypeCondition = new List<KeyValuePair<string, string>>() {
             new KeyValuePair<string, string>("Image", "0"),
                new KeyValuePair<string, string>("Video", "1"),
                new KeyValuePair<string, string>("Music", "2")

        };

        public static string[] userProfile = new string[] { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png", "7.png", "8.png", "9.png", "10.png" };

    }
}
