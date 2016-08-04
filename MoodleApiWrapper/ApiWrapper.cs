using MoodleApiWrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoodleApiWrapper
{
    public class ApiWrapper
    {


        #region Properties

        /// <summary>
        /// field that holds your api token
        /// </summary>
        private static string _apiToken;

        /// <summary>
        /// This property sets you Api token.
        /// </summary>
        public static string ApiToken
        {
            get { return _apiToken; }
            set { _apiToken = value; }
        }

        /// <summary>
        /// Repressents if the token is set.
        /// </summary>
        private static bool TokenIsSet => ApiToken.Any();

        private static Uri _host;

        public static Uri Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// Represents if the host address is set.
        /// </summary>
        private static bool HostIsSet => Host.AbsoluteUri.Any();

        #endregion

        #region Parse methods

        private static string ParseFormat(Format format)
        {
            switch (format)
            {
                case Format.JSON:
                    return "json";
                case Format.XML:
                    return "xml";
            }
            throw new ArgumentOutOfRangeException("format");
        }


        private static string ParseMethod(Methods method)
        {
            switch (method)
            {
                case Methods.core_webservice_get_site_info:
                    return "core_webservice_get_site_info";
                case Methods.core_user_get_users:
                    return "core_user_get_users";
                case Methods.core_user_get_users_by_field:
                    return "core_user_get_users_by_field";
                case Methods.core_enrol_get_users_courses:
                    return "core_enrol_get_users_courses";
                case Methods.core_user_create_users:
                    return "core_user_create_users";
                case Methods.core_user_update_users:
                    return "core_user_update_users";
                case Methods.core_user_delete_users:
                    return "core_user_delete_users";
                case Methods.core_role_assign_roles:
                    return "core_role_assign_roles";
                case Methods.core_role_unassign_roles:
                    return "core_role_unassign_roles";
                case Methods.enrol_manual_enrol_users:
                    return "enrol_manual_enrol_users";
                case Methods.core_group_add_group_members:
                    return "core_group_add_group_members";
                case Methods.core_group_delete_group_members:
                    return "core_group_delete_group_members";
                case Methods.core_course_get_categories:
                    return "core_course_get_categories";
                case Methods.core_course_get_courses:
                    return "core_course_get_courses";
                case Methods.core_course_get_contents:
                    return "core_course_get_contents";
                case Methods.core_group_get_groups:
                    return "core_group_get_groups";
                case Methods.core_group_get_course_groups:
                    return "core_group_get_course_groups";
                case Methods.core_enrol_get_enrolled_users:
                    return "core_enrol_get_enrolled_users";
                case Methods.core_course_create_courses:
                    return "core_course_create_courses";
                case Methods.core_course_update_courses:
                    return "core_course_update_courses";
                case Methods.core_grades_get_grades:
                    return "core_grades_get_grades";
                case Methods.core_grades_update_grades:
                    return "core_grades_update_grades";
                case Methods.core_grading_get_definitions:
                    return "core_grading_get_definitions";
                case Methods.core_calendar_get_calendar_events:
                    return "core_calendar_get_calendar_events";
                case Methods.core_calendar_create_calendar_events:
                    return "core_calendar_create_calendar_events";
                case Methods.core_calendar_delete_calendar_events:
                    return "core_calendar_delete_calendar_events";
                case Methods.default_:
                    return "";
            }
            throw new ArgumentOutOfRangeException("method");
        }

        #endregion

        #region functions

        private static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        #region Authentications
        /// <summary>
        /// Returns your Api Token needed to make any calls
        /// <para />
        /// service shortname - The service shortname is usually hardcoded in the pre-build service (db/service.php files).
        /// Moodle administrator will be able to edit shortnames for service created on the fly: MDL-29807.
        /// If you want to use the Mobile service, its shortname is moodle_mobile_app. Also useful to know,
        /// the database shortname field can be found in the table named external_services.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="serviceHostName"></param>
        /// <returns></returns>
        public static Task<AuthentiactionResponse<AuthToken>> GetApiToken(string username, string password,
            string serviceHostName)
        {
            if (HostIsSet)
            {
                string query =
                    "login/token.php" +
                    $"?username={username}" +
                    $"&password={password}" +
                    $"&service={serviceHostName}";

                return GetAuth<AuthToken>(Host.AbsoluteUri + query);
            }
            else
            {
                throw new Exception("Host is not set");
            }
        }
#endregion

        #region System actions
        /// <summary>
        /// This API will return information about the site, web services users, and authorized API actions. This call is useful for getting site information and the capabilities of the web service user. 
        /// </summary>
        /// <param name="serviceHostNames">Returns information about a particular service.</param>
        /// <returns></returns>
        public static Task<ApiResponse<Site_info>> GetSiteInfo(string serviceHostName = "")
        {
            if (HostIsSet && TokenIsSet)
            {
                var query = string.Empty;
                if (!serviceHostName.Any())
                {
                    query = string.Format("webservice/rest/server.php" +
                                          "?wstoken={0}&moodlewsrestformat={1}&wsfunction={2}",
                        ApiToken, ParseFormat(Format.JSON), ParseMethod(Methods.core_webservice_get_site_info));
                }
                else
                {
                    query = string.Format("webservice/rest/server.php" +
                                          "?wstoken={0}&moodlewsrestformat={1}&wsfunction={2}&serviceshortnames[0]={3}",
                        ApiToken, ParseFormat(Format.JSON), ParseMethod(Methods.core_webservice_get_site_info),
                        serviceHostName);
                }

                return Get<Site_info>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }
        #endregion

        #region User Actions 
        /// <summary>
        /// Search for users matching the parameters of the call. This call will return matching user accounts with profile fields.
        ///  The key/value pairs to be considered in user search. Values can not be empty. Specify different keys only once
        ///  (fullname =&gt; 'user1', auth =&gt; 'manual', ...) - key occurences are forbidden. The search is executed with AND operator on the criterias.
        ///  Invalid criterias (keys) are ignored, the search is still executed on the valid criterias. You can search without criteria,
        ///  but the function is not designed for it. It could very slow or timeout. The function is designed to search some specific users.
        /// <para />
        /// "id" (int) matching user id<para />
        ///"lastname" (string) user last name (Note: you can use % for searching but it may be considerably slower!)<para />
        ///"firstname" (string) user first name (Note: you can use % for searching but it may be considerably slower!)<para />
        ///"idnumber" (string) matching user idnumber<para />
        ///"username" (string) matching user username<para />
        ///"email" (string) user email (Note: you can use % for searching but it may be considerably slower!)<para />
        ///"auth" (string) matching user auth plugin<para />
        /// </summary>
        /// <param name="criteria_key0">Key of the first search parameter.</param>
        /// <param name="criteria_value0">Value of the first search term.</param>
        /// <param name="criteria_key1">Key of the second search parameter.</param>
        /// <param name="criteria_value1">Value of the second search term.</param>
        /// <returns></returns>
        public static Task<ApiResponse<Users>> GetUsers(string criteria_key0, string criteria_value0,
            string criteria_key1 = "", string criteria_value1 = "")
        {
            if (HostIsSet && TokenIsSet)
            {
                string query = string.Empty;
                if (criteria_key1.Any() && criteria_value1.Any())
                {
                    query =
                        "webservice/rest/server.php?" +
                        $"wstoken={ApiToken}&" +
                        $"wsfunction={ParseMethod(Methods.core_user_get_users)}&" +
                        $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                        $"criteria[0][key]={criteria_key0}&" +
                        $"criteria[0][value]={criteria_value0}" +
                        $"criteria[1][key]={criteria_key0}&" +
                        $"criteria[1][value]={criteria_value0}";
                }
                else
                {
                    query =
                        "webservice/rest/server.php?" +
                        $"wstoken={ApiToken}&" +
                        $"wsfunction={ParseMethod(Methods.core_user_get_users)}&" +
                        $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                        $"criteria[0][key]={criteria_key0}&" +
                        $"criteria[0][value]={criteria_value0}";
                }
                return Get<Users>(Host.AbsoluteUri + query);


            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Retrieve users information for a specified unique field - If you want to do a user search, use GetUsers()
        /// 
        /// Avaiable Criteria:
        ///"id" (int) matching user id
        ///"lastname" (string) user last name (Note: you can use % for searching but it may be considerably slower!)
        ///"firstname" (string) user first name (Note: you can use % for searching but it may be considerably slower!)
        ///"idnumber" (string) matching user idnumber
        ///"username" (string) matching user username
        ///"email" (string) user email (Note: you can use % for searching but it may be considerably slower!)
        ///"auth" (string) matching user auth plugin
        /// </summary>
        /// <param name="criteria_key">Key of the first search parameter.</param>
        /// <param name="criteria_value">Value of the first search term.</param>
        /// <returns></returns>
        public static Task<ApiResponse<Users>> GetUsersByField(string criteria_key, string criteria_value)
        {
            if (HostIsSet && TokenIsSet)
            {
                string query = string.Empty;
                query =
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_user_get_users_by_field)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"criteria[0][key]={criteria_key}&" +
                    $"criteria[0][value]={criteria_value}";

                return Get<Users>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Get the list of courses where a user is enrolled in 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Cources>> GetUserCourses(int userid)
        {
            if (HostIsSet && TokenIsSet)
            {
                string query = string.Empty;
                query =
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_enrol_get_users_courses)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"userid={userid}";

                return Get<Cources>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Create a User.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="auth"></param>
        /// <param name="idnumber"></param>
        /// <param name="lang"></param>
        /// <param name="calendartye"></param>
        /// <param name="theme"></param>
        /// <param name="timezone"></param>
        /// <param name="mailformat"></param>
        /// <param name="description"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="firstnamephonetic"></param>
        /// <param name="lastnamephonetic"></param>
        /// <param name="middlename"></param>
        /// <param name="alternatename"></param>
        /// <param name="preferences_type"></param>
        /// <param name="preferences_value"></param>
        /// <param name="customfields_type"></param>
        /// <param name="customfields_value"></param>
        /// <returns></returns>
        public static Task<ApiResponse<NewUser>> CreateUser(string username, string firstname, string lastname,
            string email, string password,
            string auth = "", string idnumber = "", string lang = "", string calendartye = "", string theme = "",
            string timezone = "",
            string mailformat = "", string description = "", string city = "", string country = "",
            string firstnamephonetic = "",
            string lastnamephonetic = "", string middlename = "", string alternatename = "",
            string preferences_type = "", string preferences_value = "",
            string customfields_type = "", string customfields_value = "")
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder querybuilder = new StringBuilder();
                querybuilder.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&wsfunction={ParseMethod(Methods.core_user_create_users)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"users[0][username]={@username}&" +
                    $"users[0][password]={@password}&" +
                    $"users[0][firstname]={@firstname}&" +
                    $"users[0][lastname]={@lastname}&" +
                    $"users[0][email]={@email}");
                if (auth.Any()) querybuilder.Append($"&users[0][auth]={auth}");
                if (idnumber.Any()) querybuilder.Append($"&users[0][auth]={idnumber}");
                if (lang.Any()) querybuilder.Append($"&users[0][auth]={lang}");
                if (calendartye.Any()) querybuilder.Append($"&users[0][auth]={calendartye}");
                if (theme.Any()) querybuilder.Append($"&users[0][auth]={theme}");
                if (timezone.Any()) querybuilder.Append($"&users[0][auth]={timezone}");
                if (mailformat.Any()) querybuilder.Append($"&users[0][auth]={mailformat}");
                if (description.Any()) querybuilder.Append($"&users[0][auth]={description}");
                if (city.Any()) querybuilder.Append($"&users[0][auth]={city}");
                if (country.Any()) querybuilder.Append($"&users[0][auth]={country}");
                if (firstnamephonetic.Any()) querybuilder.Append($"&users[0][auth]={firstnamephonetic}");
                if (lastnamephonetic.Any()) querybuilder.Append($"&users[0][auth]={lastnamephonetic}");
                if (middlename.Any()) querybuilder.Append($"&users[0][auth]={middlename}");
                if (alternatename.Any()) querybuilder.Append($"&users[0][auth]={alternatename}");
                if (preferences_type.Any()) querybuilder.Append($"&users[0][auth]={preferences_type}");
                if (preferences_value.Any()) querybuilder.Append($"&users[0][auth]={preferences_value}");
                if (customfields_type.Any()) querybuilder.Append($"&users[0][auth]={customfields_type}");
                if (customfields_value.Any()) querybuilder.Append($"&users[0][auth]={customfields_value}");


                return Get<NewUser>(Host.AbsoluteUri + querybuilder.ToString());
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }


        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="auth"></param>
        /// <param name="idnumber"></param>
        /// <param name="lang"></param>
        /// <param name="calendartye"></param>
        /// <param name="theme"></param>
        /// <param name="timezone"></param>
        /// <param name="mailformat"></param>
        /// <param name="description"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="firstnamephonetic"></param>
        /// <param name="lastnamephonetic"></param>
        /// <param name="middlename"></param>
        /// <param name="alternatename"></param>
        /// <param name="preferences_type"></param>
        /// <param name="preferences_value"></param>
        /// <param name="customfields_type"></param>
        /// <param name="customfields_value"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> UpdateUser(int id, string username = "", string firstname = "",
            string lastname = "",
            string email = "", string password = "", string auth = "", string idnumber = "", string lang = "",
            string calendartye = "", string theme = "",
            string timezone = "", string mailformat = "", string description = "", string city = "", string country = "",
            string firstnamephonetic = "",
            string lastnamephonetic = "", string middlename = "", string alternatename = "",
            string preferences_type = "", string preferences_value = "",
            string customfields_type = "", string customfields_value = "")
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder querybuilder = new StringBuilder();
                querybuilder.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&wsfunction={ParseMethod(Methods.core_user_update_users)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"users[0][id]={id}");

                if (username.Any()) querybuilder.Append($"&users[0][username]={username}");
                if (password.Any()) querybuilder.Append($"&users[0][password]={password}");
                if (firstname.Any()) querybuilder.Append($"&users[0][firstname]={firstname}");
                if (lastname.Any()) querybuilder.Append($"&users[0][lastname]={lastname}");
                if (email.Any()) querybuilder.Append($"&users[0][email]={email}");
                if (auth.Any()) querybuilder.Append($"&users[0][auth]={auth}");
                if (idnumber.Any()) querybuilder.Append($"&users[0][auth]={idnumber}");
                if (lang.Any()) querybuilder.Append($"&users[0][auth]={lang}");
                if (calendartye.Any()) querybuilder.Append($"&users[0][auth]={calendartye}");
                if (theme.Any()) querybuilder.Append($"&users[0][auth]={theme}");
                if (timezone.Any()) querybuilder.Append($"&users[0][auth]={timezone}");
                if (mailformat.Any()) querybuilder.Append($"&users[0][auth]={mailformat}");
                if (description.Any()) querybuilder.Append($"&users[0][auth]={description}");
                if (city.Any()) querybuilder.Append($"&users[0][auth]={city}");
                if (country.Any()) querybuilder.Append($"&users[0][auth]={country}");
                if (firstnamephonetic.Any()) querybuilder.Append($"&users[0][auth]={firstnamephonetic}");
                if (lastnamephonetic.Any()) querybuilder.Append($"&users[0][auth]={lastnamephonetic}");
                if (middlename.Any()) querybuilder.Append($"&users[0][auth]={middlename}");
                if (alternatename.Any()) querybuilder.Append($"&users[0][auth]={alternatename}");
                if (preferences_type.Any()) querybuilder.Append($"&users[0][auth]={preferences_type}");
                if (preferences_value.Any()) querybuilder.Append($"&users[0][auth]={preferences_value}");
                if (customfields_type.Any()) querybuilder.Append($"&users[0][auth]={customfields_type}");
                if (customfields_value.Any()) querybuilder.Append($"&users[0][auth]={customfields_value}");


                return Get<Success>(Host.AbsoluteUri + querybuilder.ToString());
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> DeleteUser(int id)
        {
            if (HostIsSet && TokenIsSet)
            {
                string query = string.Empty;
                query =
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_user_delete_users)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"userids[0]={id}";

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }

        }

        /// <summary>
        /// Manual role assignments. This call should be made in an array. 
        /// </summary>
        /// <param name="role_id">
        /// <summary>Role to assign to the user</summary>
        /// </param>
        /// <param name="user_id"></param>
        /// <param name="context_id"></param>
        /// <param name="context_level"></param>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> AssignRoles(int role_id, int user_id, string context_id = "",
            string context_level = "", int instance_id = Int32.MinValue)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_role_assign_roles)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"assignments[0][roleid]={role_id}&" +
                    $"assignments[0][userid]={user_id}");
                if (context_id.Any()) query.Append($"&assignments[0][contextid]={context_id}");
                if (context_level.Any()) query.Append($"&assignments[0][contextlevel]={context_level}");
                if (instance_id != Int32.MinValue) query.Append($"&assignments[0][instanceid]={instance_id}");

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="user_id"></param>
        /// <param name="context_id"></param>
        /// <param name="context_level"></param>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> UnassignRoles(int role_id, int user_id, string context_id = "",
           string context_level = "", int instance_id = Int32.MinValue)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_role_unassign_roles)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"unassignments[0][roleid]={role_id}&" +
                    $"unassignments[0][userid]={user_id}");
                if (context_id.Any()) query.Append($"&unassignments[0][contextid]={context_id}");
                if (context_level.Any()) query.Append($"&unassignments[0][contextlevel]={context_level}");
                if (instance_id != Int32.MinValue) query.Append($"&unassignments[0][instanceid]={instance_id}");

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        #endregion

        #region Course Enrollment Actions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="user_id"></param>
        /// <param name="cource_id"></param>
        /// <param name="timestart"></param>
        /// <param name="timeend"></param>
        /// <param name="suspend"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> EnrolUser(int role_id, int user_id, int cource_id,
                                                            int timestart = Int32.MinValue, int timeend = Int32.MinValue, int suspend = Int32.MinValue)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.enrol_manual_enrol_users)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"enrolments[0][roleid]={role_id}&" +
                    $"enrolments[0][userid]={user_id}&" + 
                    $"enrolments[0][courceid]={cource_id}");
                if (timestart != Int32.MinValue) query.Append($"&enrolments[0][timestart]={timestart}");
                if (timeend != Int32.MinValue) query.Append($"&enrolments[0][timeend]={timeend}");
                if (suspend != Int32.MinValue) query.Append($"&enrolments[0][suspend]={suspend}");

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> AddGroupMember(int group_id, int user_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_group_add_group_members)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"members[0][groupid]={group_id}&" +
                    $"members[0][userid]={user_id}");               

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Success>> DeleteGroupMember(int group_id, int user_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_group_delete_group_members)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"members[0][groupid]={group_id}&" +
                    $"members[0][userid]={user_id}");

                return Get<Success>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }


        #endregion

        #region Course Actions
       
        /// <summary>
        /// Get a listing of categories in the system. 
        /// </summary>
        /// <param name="criteria_key">
        /// <summary>
        /// criteria[0][key] - The category column to search, expected keys (value format) are:"id" (int) the category id,"name" (string)
        ///  the category name,"parent" (int) the parent category id,"idnumber" (string) category idnumber - user must have 'moodle/category:manage'
        ///  to search on idnumber,"visible" (int) whether the returned categories must be visible or hidden.
        ///  If the key is not passed, then the function return all categories that the user can see. - user must have 'moodle/category:manage'
        ///  or 'moodle/category:viewhiddencategories' to search on visible,"theme" (string) only return the categories having this theme
        ///  - user must have 'moodle/category:manage' to search on theme
        /// </summary>
        /// </param>
        /// <param name="criteria_value"><summary>Criteria[0][value] - The value to match</summary></param>
        /// <param name="addSubCategories"><summary>Return the sub categories infos (1 - default) otherwise only the category info (0)</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<Category>> GetCategories(string criteria_key, string criteria_value, int addSubCategories = 1 )
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_get_categories)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"criteria[0][key]={criteria_key}&" +
                    $"criteria[0][value]={criteria_value}");

                if (addSubCategories != 1) query.Append($"&addsubcategories={addSubCategories}");

                return Get<Category>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Get a listing of courses in the system. 
        /// </summary>
        /// <param name="options"><summary>List of course id.If empty return all courses except front page course.</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<Course>> GetCourses(int options = int.MinValue)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_get_courses)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}"); 
                    if (options != int.MinValue) query.Append($"&addsubcategories={options}");

                return Get<Course>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Get course contents
        /// </summary>
        /// <param name="course_id"><summary>Course Id</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<Content>> GetContents(int course_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_get_contents)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&"+
                    $"courseid={course_id}");
                
                return Get<Content>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Returns group details. 
        /// </summary>
        /// <param name="group_id">Group id</param>
        /// <returns></returns>
        public static Task<ApiResponse<Group>> GetGroup(int group_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_group_get_groups)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"groupids[0]={group_id}");

                return Get<Group>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }
        /// <summary>
        /// Returns group details. 
        /// </summary>
        /// <param name="group_ids"><summary>Group Ids</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<Group>> GetGroups(int[] group_ids)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_group_get_groups)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}");

                for (int i = 0; i < group_ids.Count(); i++)
                {
                    query.Append($"&groupids[{i}]={group_ids[i]}");
                }

                return Get<Group>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Returns all groups in specified course. 
        /// </summary>
        /// <param name="course_id"><summary>Course Id</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<Group>> GetCourseGroups(int course_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_group_get_course_groups)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&"+
                    $"courseid={course_id}");

                return Get<Group>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Get enrolled users by course id. 
        /// </summary>
        /// <param name="course_id"></param>
        /// <returns></returns>
        public static Task<ApiResponse<EnrolledUser>> GetEnrolledUsersByCourse(int course_id)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_enrol_get_enrolled_users)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"courseid={course_id}");

                return Get<EnrolledUser>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="fullname"><summary>Full name of the course</summary></param>
        /// <param name="shortname"><summary>Shortname of the course</summary></param>
        /// <param name="category_id"><summary>Category ID of the course</summary></param>
        /// <param name="idnumber"><summary>Optional //id number</summary></param>
        /// <param name="summary"><summary>Optional //summary</summary></param>
        /// <param name="summaryformat"><summary>Default to "1" //summary format (1 = HTML, 0 = MOODLE, 2 = PLAIN or 4 = MARKDOWN)</summary></param>
        /// <param name="format"><summary>Default to "topics" //course format: weeks, topics, social, site,..</summary></param>
        /// <param name="showgrades"><summary>Default to "0" //1 if grades are shown, otherwise 0</summary></param>
        /// <param name="newsitems"><summary>Default to "0" //number of recent items appearing on the course page</summary></param>
        /// <param name="startdate"><summary>Optional //timestamp when the course start</summary></param>
        /// <param name="numsections"><summary>Optional //(deprecated, use courseformatoptions) number of weeks/topics</summary></param>
        /// <param name="maxbytes"><summary>Default to "104857600" //largest size of file that can be uploaded into the course</summary></param>
        /// <param name="showreports"><summary>Default to "1" //are activity report shown (yes = 1, no =0)</summary></param>
        /// <param name="visible"><summary>Optional //1: available to student, 0:not available</summary></param>
        /// <param name="hiddensections"><summary>Optional //(deprecated, use courseformatoptions) How the hidden sections in the course are displayed to students</summary></param>
        /// <param name="groupmode"><summary>Default to "0" //no group, separate, visible</summary></param>
        /// <param name="groupmodeforce"><summary>Default to "0" //1: yes, 0: no</summary></param>
        /// <param name="defaultgroupingid"><summary>Default to "0" //default grouping id</summary></param>
        /// <param name="enablecompletion"><summary>Optional //Enabled, control via completion and activity settings. Disabled, not shown in activity settings.</summary></param>
        /// <param name="completenotify"><summary>Optional //1: yes 0: no</summary></param>
        /// <param name="lang"><summary>//forced course language</summary></param>
        /// <param name="forcetheme"><summary>Optional //name of the force theme</summary></param>
        /// <param name="courcCourseformatoption"><summary>Optional //additional options for particular course format list of ( object { name string //course format option name
        ///value string //course format option value } )} )</summary></param>
        /// <returns></returns>
        public static Task<ApiResponse<NewCourse>> CreateCourse(string fullname, string shortname, int category_id,
            string idnumber = "", string summary = "", int summaryformat = 1, string format = "", int showgrades = 0, int newsitems = 0,
            DateTime startdate = default(DateTime), int numsections = int.MaxValue, int maxbytes = 104857600, int showreports = 1, 
            int visible = 0, int hiddensections = int.MaxValue, int groupmode = 0,
            int groupmodeforce = 0, int defaultgroupingid = 0, int enablecompletion = int.MaxValue,
            int completenotify = 0, string lang = "", string forcetheme = "",
            string courcCourseformatoption = ""/*not implemented*/)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_create_courses)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"courses[0][fullname]={fullname}&"+
                    $"courses[0][shortname]={shortname}&"+
                    $"courses[0][categoryid]={category_id}");

                if(idnumber.Any()) query.Append($"&courses[0][idnumber]={idnumber}");
                if (summary.Any()) query.Append($"&courses[0][summary]={summary}");
                if (summaryformat != 1) query.Append($"&courses[0][summaryformat ]={summaryformat}");
                if (format.Any()) query.Append($"&courses[0][format]={format}");
                if (showgrades != 0) query.Append($"&courses[0][showgrades]={showgrades}");
                if (!startdate.Equals(default(DateTime))) query.Append($"&courses[0][startdate]={DateTimeToUnixTimestamp(startdate)}");
                if (newsitems!=0) query.Append($"&courses[0][newsitems]={newsitems}");
                if (numsections != int.MaxValue) query.Append($"&courses[0][numsections]={numsections}");
                if (maxbytes != 104857600) query.Append($"&courses[0][maxbytes]={category_id}");
                if (showreports != 1) query.Append($"&courses[0][showreports]={showreports}");
                if (visible != 0) query.Append($"&courses[0][visible]={visible}");
                if (hiddensections != int.MaxValue) query.Append($"&courses[0][hiddensections]={hiddensections}");
                if (groupmode != 0) query.Append($"&courses[0][groupmode]={groupmode}");
                if (groupmodeforce != 0) query.Append($"&courses[0][groupmodeforce]={groupmodeforce}");
                if (defaultgroupingid != 0) query.Append($"&courses[0][defaultgroupingid]={defaultgroupingid}");
                if (enablecompletion != int.MaxValue) query.Append($"&courses[0][enablecompletion]={enablecompletion}");
                if (completenotify != 0) query.Append($"&courses[0][completenotify]={completenotify}");
                if (lang.Any()) query.Append($"&courses[0][lang]={lang}");
                if (forcetheme.Any()) query.Append($"&courses[0][forcetheme]={forcetheme}");

                return Get<NewCourse>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }
        /// <summary>
        /// Create new courses
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="shortname"></param>
        /// <param name="category_ids"></param>
        /// <returns></returns>
        public static Task<ApiResponse<NewCourse>> CreateCourses(string[] fullname, string[] shortname,int[] category_ids)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_create_courses)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}");
                for (int i = 0; i < fullname.Count(); i++)
                {
                    query.Append(
                        $"&courses[{i}][fullname]={fullname[i]}" +
                        $"&courses[{i}][shortname]={shortname[i]}" +
                        $"&courses[{i}][categoryid]={category_ids[i]}");
                }

                return Get<NewCourse>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        public static Task<ApiResponse<UpdateCourseRoot>> UpdateCourse(int id, string fullname = "", string shortname = "", int category_id = Int32.MaxValue,
    string idnumber = "", string summary = "", int summaryformat = 1, string format = "", int showgrades = 0, int newsitems = 0,
    DateTime startdate = default(DateTime), int numsections = int.MaxValue, int maxbytes = 104857600, int showreports = 1,
    int visible = 0, int hiddensections = int.MaxValue, int groupmode = 0,
    int groupmodeforce = 0, int defaultgroupingid = 0, int enablecompletion = int.MaxValue,
    int completenotify = 0, string lang = "", string forcetheme = "",
    string courcCourseformatoption = ""/*not implemented*/)
        {
            if (HostIsSet && TokenIsSet)
            {
                StringBuilder query = new StringBuilder();
                query.Append(
                    "webservice/rest/server.php?" +
                    $"wstoken={ApiToken}&" +
                    $"wsfunction={ParseMethod(Methods.core_course_update_courses)}&" +
                    $"moodlewsrestformat={ParseFormat(Format.JSON)}&" +
                    $"courses[0][id]={id}");

                if (fullname.Any()) query.Append($"&courses[0][fullname]={fullname}");
                if (shortname.Any()) query.Append($"&courses[0][shortname]={shortname}");
                if (category_id != Int32.MaxValue) query.Append($"&courses[0][categoryid]={category_id}");
                if (idnumber.Any()) query.Append($"&courses[0][idnumber]={idnumber}");
                if (summary.Any()) query.Append($"&courses[0][summary]={summary}");
                if (summaryformat != 1) query.Append($"&courses[0][summaryformat ]={summaryformat}");
                if (format.Any()) query.Append($"&courses[0][format]={format}");
                if (showgrades != 0) query.Append($"&courses[0][showgrades]={showgrades}");
                if (!startdate.Equals(default(DateTime))) query.Append($"&courses[0][startdate]={DateTimeToUnixTimestamp(startdate)}");
                if (newsitems != 0) query.Append($"&courses[0][newsitems]={newsitems}");
                if (numsections != int.MaxValue) query.Append($"&courses[0][numsections]={numsections}");
                if (maxbytes != 104857600) query.Append($"&courses[0][maxbytes]={category_id}");
                if (showreports != 1) query.Append($"&courses[0][showreports]={showreports}");
                if (visible != 0) query.Append($"&courses[0][visible]={visible}");
                if (hiddensections != int.MaxValue) query.Append($"&courses[0][hiddensections]={hiddensections}");
                if (groupmode != 0) query.Append($"&courses[0][groupmode]={groupmode}");
                if (groupmodeforce != 0) query.Append($"&courses[0][groupmodeforce]={groupmodeforce}");
                if (defaultgroupingid != 0) query.Append($"&courses[0][defaultgroupingid]={defaultgroupingid}");
                if (enablecompletion != int.MaxValue) query.Append($"&courses[0][enablecompletion]={enablecompletion}");
                if (completenotify != 0) query.Append($"&courses[0][completenotify]={completenotify}");
                if (lang.Any()) query.Append($"&courses[0][lang]={lang}");
                if (forcetheme.Any()) query.Append($"&courses[0][forcetheme]={forcetheme}");

                return Get<UpdateCourseRoot>(Host.AbsoluteUri + query);
            }
            else
            {
                if (!HostIsSet && TokenIsSet)
                    throw new Exception("Host & token are not set");
                else if (!HostIsSet)
                    throw new Exception("Host is not set");
                else
                    throw new Exception("Token is not set");
            }
        }

        #endregion

        #region Getters
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static async Task<AuthentiactionResponse<T>> GetAuth<T>(string uri) where T : IDataModel
        {
            try
            {
                var request = WebRequest.Create(Uri.EscapeUriString(uri));
                using (var response = await request.GetResponseAsync())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var data = JObject.Parse(await reader.ReadToEndAsync());
                        return new AuthentiactionResponse<T>(new AuthentiactionResponseRaw(data));
                    }
                }
            }
            catch (WebException)
            {
                // No internet connection
                throw new WebException("No internet connection.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static async Task<ApiResponse<T>> Get<T>(string uri) where T : IDataModel
        {
            try
            {
                var request = WebRequest.Create(Uri.EscapeUriString(uri));
                using (var response = await request.GetResponseAsync())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = await reader.ReadToEndAsync();
                        if (result.ToLower() == "null")
                            result = "{IsSuccessful: true,}";
                        try
                        {
                            var data = JArray.Parse(result);
                            return new ApiResponse<T>(new ApiResponseRaw(data));
                        }
                        catch(Exception ex)
                        {
                            var data = JObject.Parse(result);
                            return new ApiResponse<T>(new ApiResponseRaw(data));
                        }
                    }
                }
            }
            catch (WebException)
            {
                // No internet connection
                throw new WebException("No internet connection.");
            }
        }
        #endregion
        
        #endregion
    }
}

