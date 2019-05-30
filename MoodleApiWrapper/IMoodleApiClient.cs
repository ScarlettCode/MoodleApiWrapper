using System;
using System.Text;
using System.Threading.Tasks;

namespace MoodleApiWrapper
{
    public interface IMoodleApiClient
    {
        Task<ApiResponse<Success>> AddGroupMember(int group_id, int user_id);
        Task<ApiResponse<Success>> AssignRoles(int role_id, int user_id, string context_id = "", string context_level = "", int instance_id = int.MinValue);
        Task<ApiResponse<Events>> CreateCalanderEvents(string[] names, string[] descriptions = null, int[] formats = null, int[] groupids = null, int[] courseids = null, int[] repeats = null, string[] eventtypes = null, DateTime[] timestarts = null, TimeSpan[] timedurations = null, int[] visible = null, int[] sequences = null);
        Task<ApiResponse<NewCourse>> CreateCourse(string fullname, string shortname, int category_id, string idnumber = "", string summary = "", int summaryformat = 1, string format = "", int showgrades = 0, int newsitems = 0, DateTime startdate = default(DateTime), int numsections = int.MaxValue, int maxbytes = 104857600, int showreports = 1, int visible = 0, int hiddensections = int.MaxValue, int groupmode = 0, int groupmodeforce = 0, int defaultgroupingid = 0, int enablecompletion = int.MaxValue, int completenotify = 0, string lang = "", string forcetheme = "", string courcCourseformatoption = "");
        Task<ApiResponse<NewCourse>> CreateCourses(string[] fullname, string[] shortname, int[] category_ids);
        Task<ApiResponse<Group>> CreateGroups(string[] names = null, int[] courseids = null, string[] descriptions = null, int[] descriptionformats = null, string[] enrolmentkeys = null, string[] idnumbers = null);
        Task<ApiResponse<NewUser>> CreateUser(string username, string firstname, string lastname, string email, string password, string auth = "", string idnumber = "", string lang = "", string calendartye = "", string theme = "", string timezone = "", string mailformat = "", string description = "", string city = "", string country = "", string firstnamephonetic = "", string lastnamephonetic = "", string middlename = "", string alternatename = "", string preferences_type = "", string preferences_value = "", string customfields_type = "", string customfields_value = "");
        Task<ApiResponse<Events>> DeleteCalanderEvents(int[] eventids, int[] repeats, string[] descriptions = null);
        Task<ApiResponse<Success>> DeleteGroupMember(int group_id, int user_id);
        Task<ApiResponse<Success>> DeleteUser(int id);
        Task<ApiResponse<Success>> EnrolUser(int role_id, int user_id, int cource_id, int timestart = int.MinValue, int timeend = int.MinValue, int suspend = int.MinValue);
        Task<AuthentiactionResponse<AuthToken>> GetApiToken(string username, string password, string serviceHostName);
        Task<ApiResponse<Events>> GetCalanderEvents(int[] groupids = null, int[] courseids = null, int[] eventids = null);
        Task<ApiResponse<Category>> GetCategories(string criteria_key, string criteria_value, int addSubCategories = 1);
        Task<ApiResponse<Content>> GetContents(int course_id);
        Task<ApiResponse<Group>> GetCourseGroups(int course_id);
        Task<ApiResponse<Course>> GetCourses(int options = int.MinValue);
        Task<ApiResponse<EnrolledUser>> GetEnrolledUsersByCourse(int course_id);
        Task<ApiResponse<Category>> GetGrades(int courseid, string component = "", int activityid = int.MaxValue, string[] userids = null);
        Task<ApiResponse<Group>> GetGroup(int group_id);
        Task<ApiResponse<Group>> GetGroups(int[] group_ids);
        Task<ApiResponse<Site_info>> GetSiteInfo(string serviceHostName = "");
        Task<ApiResponse<Cources>> GetUserCourses(int userid);
        Task<ApiResponse<Users>> GetUsers(string criteria_key0, string criteria_value0, string criteria_key1 = "", string criteria_value1 = "");
        Task<ApiResponse<Users>> GetUsersByField(string criteria_key, string criteria_value);
        Task<ApiResponse<Success>> UnassignRoles(int role_id, int user_id, string context_id = "", string context_level = "", int instance_id = int.MinValue);
        Task<ApiResponse<UpdateCourseRoot>> UpdateCourse(int id, string fullname = "", string shortname = "", int category_id = int.MaxValue, string idnumber = "", string summary = "", int summaryformat = 1, string format = "", int showgrades = 0, int newsitems = 0, DateTime startdate = default(DateTime), int numsections = int.MaxValue, int maxbytes = 104857600, int showreports = 1, int visible = 0, int hiddensections = int.MaxValue, int groupmode = 0, int groupmodeforce = 0, int defaultgroupingid = 0, int enablecompletion = int.MaxValue, int completenotify = 0, string lang = "", string forcetheme = "", string courcCourseformatoption = "");
        Task<ApiResponse<Success>> UpdateUser(int id, string username = "", string firstname = "", string lastname = "", string email = "", string password = "", string auth = "", string idnumber = "", string lang = "", string calendartye = "", string theme = "", string timezone = "", string mailformat = "", string description = "", string city = "", string country = "", string firstnamephonetic = "", string lastnamephonetic = "", string middlename = "", string alternatename = "", string preferences_type = "", string preferences_value = "", string customfields_type = "", string customfields_value = "");

        Task<ApiResponse<Site_info>> GetSignupSettings();

        /// <summary>
        /// Execute custom method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Method name (ex core_group_create_groups)</param>
        /// <param name="queryBuilder">Add parameters to the query (ex query.Append($"&groups[{i}][idnumber]={idnumbers[i]}"))</param>
        /// <returns></returns>
        Task<ApiResponse<T>> ExecuteMethod<T>(string methodName, Action<StringBuilder> queryBuilder = null) where T : IDataModel;
    }
}