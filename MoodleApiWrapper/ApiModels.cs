using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MoodleApiWrapper
{
    public interface IDataModel
    {

    }

    public enum Methods
    {
        core_webservice_get_site_info,
        core_user_get_users,
        core_user_get_users_by_field,
        core_enrol_get_users_courses,
        core_user_create_users,
        core_user_update_users,
        core_user_delete_users,
        core_role_assign_roles,
        core_role_unassign_roles,
        //Course Enrollment Actions
        enrol_manual_enrol_users,
        core_group_add_group_members,
        core_group_delete_group_members,
        //Course Actions
        core_course_get_categories,
        core_course_get_courses,
        core_course_get_contents,
        core_group_get_groups,
        core_group_get_course_groups,
        core_enrol_get_enrolled_users,
        core_course_create_courses,
        core_course_update_courses,
        //Grade Actions
        core_grades_get_grades,
        core_grades_update_grades,
        core_grading_get_definitions,
        //Calendar Actions
        core_calendar_get_calendar_events,
        core_calendar_create_calendar_events,
        core_calendar_delete_calendar_events,
        default_

    }
    /// <summary>
    /// Represents the format of the response.
    /// </summary>
    public enum Format
    {
        JSON,
        XML
    }

    public class Function : ICloneable
    {
        public string name { get; set; }
        public string version { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [JsonConstructor]
        internal Function(string name, string version)
        {
            this.name = name;
            this.version = version;
        }
    }

    public class Role : ICloneable
    {
        [JsonConstructor]
        internal Role(int roleid, string name, string shortname, int sortorder)
        {
            this.roleid = roleid;
            this.name = name;
            this.shortname = shortname;
            this.sortorder = sortorder;
        }

        public int roleid { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public int sortorder { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Enrolledcours : ICloneable
    {
        [JsonConstructor]
        internal Enrolledcours(int id, string fullname, string shortname)
        {
            this.id = id;
            this.fullname = fullname;
            this.shortname = shortname;
        }

        public int id { get; set; }
        public string fullname { get; set; }
        public string shortname { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Advancedfeature : ICloneable
    {
        public string name { get; set; }
        public int value { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [JsonConstructor]
        internal Advancedfeature(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }

    /// <summary>
    /// Represents the data associated to the site information
    /// </summary>
    public class Site_info : ICloneable, IDataModel
    {
        public string sitename { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string lang { get; set; }
        public int userid { get; set; }
        public string siteurl { get; set; }
        public string userpictureurl { get; set; }
        public List<Function> functions { get; set; }
        public int downloadfiles { get; set; }
        public int uploadfiles { get; set; }
        public string release { get; set; }
        public string version { get; set; }
        public string mobilecssurl { get; set; }
        public List<Advancedfeature> advancedfeatures { get; set; }
        public bool usercanmanageownfiles { get; set; }
        public int userquota { get; set; }
        public int usermaxuploadfilesize { get; set; }
        public int userhomepage { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [JsonConstructor]
        internal Site_info(string sitename, string username, string firstname, string lastname, string fullname,
            string lang, int userid, string siteurl, string userpictureurl, List<Function> functions, int downloadfiles,
            int uploadfiles, string release, string version, string mobilcssurl,List<Advancedfeature> advancedfeatures,
            bool usercanmanageownfiles, int userquota, int usermaxuploadfilesize, int userhomepage)
        {
            this.sitename = sitename;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.fullname = fullname;
            this.lang = lang;
            this.userid = userid;
            this.siteurl = siteurl;
            this.userpictureurl = userpictureurl;
            this.functions = functions;
            this.downloadfiles = downloadfiles;
            this.uploadfiles = uploadfiles;
            this.release = release;
            this.version = version;
            this.mobilecssurl = mobilcssurl;
            this.advancedfeatures = advancedfeatures;
            this.usercanmanageownfiles = usercanmanageownfiles;
            this.userquota = userquota;
            this.usermaxuploadfilesize = usermaxuploadfilesize;
            this.userhomepage = userhomepage;
        }
    }

    public class Customfield : ICloneable
    {
        public string type { get; set; }
        public string value { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }

        [JsonConstructor]
        internal Customfield(string type, string value, string name, string shortname)
        {
            this.type = type;
            this.value = value;
            this.name = name;
            this.shortname = shortname;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Preference : ICloneable
    {
        public string name { get; set; }
        public object value { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [JsonConstructor]
        internal Preference(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public class User : ICloneable
    {
        [JsonConstructor]
        internal User(int id, string username, string firstname, string lastname, string fullname, string email, string department,
            int firstaccess, int lastaccess, string description, int descriptionformat, string profileimageurlsmall, string profileimageurl,
            string country,List<Customfield> customfields ,List<Preference> preferences)
        {
            this.id = id;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.fullname = fullname;
            this.email = email;
            this.department = department;
            this.firstaccess = firstaccess;
            this.lastaccess = lastaccess;
            this.description = description;
            this.descriptionformat = descriptionformat;
            this.profileimageurlsmall = profileimageurlsmall;
            this.profileimageurl = profileimageurl;
            this.country = country;
            this.customfields = customfields;
            this.preferences = preferences;
        }

        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public int firstaccess { get; set; }
        public int lastaccess { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
        public string profileimageurlsmall { get; set; }
        public string profileimageurl { get; set; }
        public string country { get; set; }
        public List<Customfield> customfields { get; set; }
        public List<Preference> preferences { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    
    }

    public class Warning : ICloneable
    {
        [JsonConstructor]
        internal Warning(string item, string warningcode, string message, int itemid)
        {
            this.item = item;
            this.itemid = itemid;
            this.warningcode = warningcode;
            this.message = message;
        }

       public int itemid { get; set; }
        public string item { get; set; }
        public string warningcode { get; set; }
        public string message { get; set; }

        public object Clone()
        {
           return this.MemberwiseClone();
        }
    }

    public class Users : ICloneable, IDataModel
    {
        [JsonConstructor]
        internal Users(List<User> users, List<Warning> warnings)
        {
            this.users = users;
            this.warnings = warnings;
        }

        public List<User> users { get; set; }
        public List<Warning> warnings { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Error : ICloneable
    {
        public Error(string exception, string errorcode, string message)
        {
            this.exception = exception;
            this.errorcode = errorcode;
            this.message = message;
        }

        public string exception { get; set; }
        public string errorcode { get; set; }
        public string message { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Courseformatoption : ICloneable
    {
        [JsonConstructor]
        internal Courseformatoption(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string name { get; set; }
        public int value { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Course : ICloneable, IDataModel
    {
        [JsonConstructor]
        internal Course(int id, string shortname, int categoryid, int categorysortorder, string fullname, string displayname, string idnumber, string summary, int summaryformat, string format, int showgrades, int newsitems, int startdate, int numsections, int maxbytes, int showreports, int visible, int groupmode, int groupmodeforce, int defaultgroupingid, int timecreated, int timemodified, int enablecompletion, int completionnotify, string lang, string forcetheme, List<Courseformatoption> courseformatoptions, int? hiddensections)
        {
            this.id = id;
            this.shortname = shortname;
            this.categoryid = categoryid;
            this.categorysortorder = categorysortorder;
            this.fullname = fullname;
            this.displayname = displayname;
            this.idnumber = idnumber;
            this.summary = summary;
            this.summaryformat = summaryformat;
            this.format = format;
            this.showgrades = showgrades;
            this.newsitems = newsitems;
            this.startdate = startdate;
            this.numsections = numsections;
            this.maxbytes = maxbytes;
            this.showreports = showreports;
            this.visible = visible;
            this.groupmode = groupmode;
            this.groupmodeforce = groupmodeforce;
            this.defaultgroupingid = defaultgroupingid;
            this.timecreated = timecreated;
            this.timemodified = timemodified;
            this.enablecompletion = enablecompletion;
            this.completionnotify = completionnotify;
            this.lang = lang;
            this.forcetheme = forcetheme;
            this.courseformatoptions = courseformatoptions;
            this.hiddensections = hiddensections;
        }

        public int id { get; set; }
        public string shortname { get; set; }
        public int categoryid { get; set; }
        public int categorysortorder { get; set; }
        public string fullname { get; set; }
        public string displayname { get; set; }
        public string idnumber { get; set; }
        public string summary { get; set; }
        public int summaryformat { get; set; }
        public string format { get; set; }
        public int showgrades { get; set; }
        public int newsitems { get; set; }
        public int startdate { get; set; }
        public int numsections { get; set; }
        public int maxbytes { get; set; }
        public int showreports { get; set; }
        public int visible { get; set; }
        public int groupmode { get; set; }
        public int groupmodeforce { get; set; }
        public int defaultgroupingid { get; set; }
        public int timecreated { get; set; }
        public int timemodified { get; set; }
        public int enablecompletion { get; set; }
        public int completionnotify { get; set; }
        public string lang { get; set; }
        public string forcetheme { get; set; }
        public List<Courseformatoption> courseformatoptions { get; set; }
        public int? hiddensections { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Cources : ICloneable, IDataModel
    {
        [JsonConstructor]
        internal Cources(int id, string shortname, string fullname, int enrolledusercount, string idnumber,
            int visible, string summary, int summaryformat, string format, bool showgrades,
            string lang, bool enablecompletion)
        {
            this.id = id;
            this.shortname = shortname;
            this.fullname = fullname;
            this.enrolledusercount = enrolledusercount;
            this.idnumber = idnumber;
            this.visible = visible;
            this.summary = summary;
            this.summaryformat = summaryformat;
            this.format = format;
            this.showgrades = showgrades;
            this.lang = lang;
            this.enablecompletion = enablecompletion;
        }

        public int id { get; set; }
        public string shortname { get; set; }
        public string fullname { get; set; }
        public int enrolledusercount { get; set; }
        public string idnumber { get; set; }
        public int visible { get; set; }
        public string summary { get; set; }
        public int summaryformat { get; set; }
        public string format { get; set; }
        public bool showgrades { get; set; }
        public string lang { get; set; }
        public bool enablecompletion { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Event : ICloneable
    {
        [JsonConstructor]
        internal Event(int id, string name, string description, int format, int courseid, int groupid, int userid, int repeatid, string modulename, int instance, string eventtype, int timestart, int timeduration, int visible, string uuid, int sequence, int timemodified, object subscriptionid)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.format = format;
            this.courseid = courseid;
            this.groupid = groupid;
            this.userid = userid;
            this.repeatid = repeatid;
            this.modulename = modulename;
            this.instance = instance;
            this.eventtype = eventtype;
            this.timestart = timestart;
            this.timeduration = timeduration;
            this.visible = visible;
            this.uuid = uuid;
            this.sequence = sequence;
            this.timemodified = timemodified;
            this.subscriptionid = subscriptionid;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int format { get; set; }
        public int courseid { get; set; }
        public int groupid { get; set; }
        public int userid { get; set; }
        public int repeatid { get; set; }
        public string modulename { get; set; }
        public int instance { get; set; }
        public string eventtype { get; set; }
        public int timestart { get; set; }
        public int timeduration { get; set; }
        public int visible { get; set; }
        public string uuid { get; set; }
        public int sequence { get; set; }
        public int timemodified { get; set; }
        public object subscriptionid { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Events : ICloneable, IDataModel
    {
        [JsonConstructor]
        internal Events(List<Event> events, List<Warning> warnings)
        {
            this.events = events;
            this.warnings = warnings;
        }

        public List<Event> events { get; set; }
        public List<Warning> warnings { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class NewUser : ICloneable,IDataModel
    {
        public int id { get; set; }
        public string username { get; set; }

        [JsonConstructor]
        internal NewUser(int id, string username)
        {
            this.id = id;
            this.username = username;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class NewCourse : ICloneable, IDataModel
    {
        public int id { get; set; }
        public string shortname { get; set; }

        [JsonConstructor]
        internal NewCourse(int id, string username)
        {
            this.id = id;
            this.shortname = username;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class UpdateCourseRoot : IDataModel, ICloneable
    {
        [JsonConstructor]
        internal UpdateCourseRoot(List<Warning> warnings)
        {
            this.warnings = warnings;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public List<Warning> warnings { get; set; }

    }

    public class AuthToken : ICloneable, IDataModel
    {
        public string token { get; set; }

        [JsonConstructor]
        internal AuthToken(string token)
        {
            this.token = token;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class AuthenticationError : IDataModel, ICloneable
    {
        [JsonConstructor]
        internal AuthenticationError(string error, object stacktrace, object debuginfo, object reproductionlink)
        {
            this.error = error;
            this.stacktrace = stacktrace;
            this.debuginfo = debuginfo;
            this.reproductionlink = reproductionlink;
        }

        public string error { get; set; }
        public object stacktrace { get; set; }
        public object debuginfo { get; set; }
        public object reproductionlink { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Success : IDataModel, ICloneable
    {
        private bool IsSuccessful { get; set; }

        [JsonConstructor]
        internal Success(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Category : IDataModel, ICloneable
    {
        [JsonConstructor]
        internal Category(int id, string name, string idnumber, string description, int descriptionformat, int parent, int sortorder, int coursecount, int visible, int visibleold, int timemodified, int depth, string path, object theme)
        {
            this.id = id;
            this.name = name;
            this.idnumber = idnumber;
            this.description = description;
            this.descriptionformat = descriptionformat;
            this.parent = parent;
            this.sortorder = sortorder;
            this.coursecount = coursecount;
            this.visible = visible;
            this.visibleold = visibleold;
            this.timemodified = timemodified;
            this.depth = depth;
            this.path = path;
            this.theme = theme;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string idnumber { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
        public int parent { get; set; }
        public int sortorder { get; set; }
        public int coursecount { get; set; }
        public int visible { get; set; }
        public int visibleold { get; set; }
        public int timemodified { get; set; }
        public int depth { get; set; }
        public string path { get; set; }
        public object theme { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Module : ICloneable
    {
        [JsonConstructor]
        internal Module(int id, string name, int visible, string modicon, string modname, string modplural, string availability, int indent, string url)
        {
            this.id = id;
            this.name = name;
            this.visible = visible;
            this.modicon = modicon;
            this.modname = modname;
            this.modplural = modplural;
            this.availability = availability;
            this.indent = indent;
            this.url = url;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int visible { get; set; }
        public string modicon { get; set; }
        public string modname { get; set; }
        public string modplural { get; set; }
        public string availability { get; set; }
        public int indent { get; set; }
        public string url { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Content : ICloneable, IDataModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int visible { get; set; }
        public string summary { get; set; }
        public int summaryformat { get; set; }
        public List<Module> modules { get; set; }

        [JsonConstructor]
        internal Content(int id, string name, int visible, string summary, int summaryformat, List<Module> modules)
        {
            this.id = id;
            this.name = name;
            this.visible = visible;
            this.summary = summary;
            this.summaryformat = summaryformat;
            this.modules = modules;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Group : ICloneable, IDataModel
    {
        [JsonConstructor]
        internal Group(int id, int courseid, string name, string description, int descriptionformat, string enrolmentkey)
        {
            this.id = id;
            this.courseid = courseid;
            this.name = name;
            this.description = description;
            this.descriptionformat = descriptionformat;
            this.enrolmentkey = enrolmentkey;
        }

        public int id { get; set; }
        public int courseid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
        public string enrolmentkey { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class EnrolledUser : IDataModel, ICloneable
    {
        [JsonConstructor]
        internal EnrolledUser(int id, string username, string firstname, string lastname, string fullname, string email, string department, int firstaccess, int lastaccess, string description, int descriptionformat, string city, string country, string profileimageurlsmall, string profileimageurl, List<Group> groups, List<Role> roles, List<Enrolledcours> enrolledcourses, List<Preference> preferences)
        {
            this.id = id;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.fullname = fullname;
            this.email = email;
            this.department = department;
            this.firstaccess = firstaccess;
            this.lastaccess = lastaccess;
            this.description = description;
            this.descriptionformat = descriptionformat;
            this.city = city;
            this.country = country;
            this.profileimageurlsmall = profileimageurlsmall;
            this.profileimageurl = profileimageurl;
            this.groups = groups;
            this.roles = roles;
            this.enrolledcourses = enrolledcourses;
            this.preferences = preferences;
        }
 
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public int firstaccess { get; set; }
        public int lastaccess { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string profileimageurlsmall { get; set; }
        public string profileimageurl { get; set; }
        public List<Group> groups { get; set; }
        public List<Role> roles { get; set; }
        public List<Enrolledcours> enrolledcourses { get; set; }
        public List<Preference> preferences { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

   
}