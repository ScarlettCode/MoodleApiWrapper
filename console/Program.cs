using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoodleApiWrapper;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSomething();


            Console.ReadLine();
        }

        private static async void DoSomething()
        {
            ApiWrapper.Host = new Uri("http://detussenschool.nl/elo/");
            ApiWrapper.ApiToken = "1e2b4d58bb7873fedb15ed705fd3e165";

           // var token = await ApiWrapper.GetApiToken("tom", "MK3$tpthggguhdwu", "services");

            var names = new string[] {"event41", "event42" , "event43" , "event44" , "event45" , "event46",
                "event21", "event22" , "event23" , "event24" , "event25" , "event26",
                "event31", "event32" , "event33" , "event34" , "event35" , "event36" };
            var starttimes = new DateTime[]
            {
                DateTime.Today.AddDays(11).AddHours(8), 
                DateTime.Today.AddDays(12).AddHours(8), 
                DateTime.Today.AddDays(12).AddHours(8), 
                DateTime.Today.AddDays(12).AddHours(8), 
                DateTime.Today.AddDays(12).AddHours(8), 
                DateTime.Today.AddDays(12).AddHours(8),

                DateTime.Today.AddDays(1).AddHours(10), 
                DateTime.Today.AddDays(2).AddHours(10), 
                DateTime.Today.AddDays(3).AddHours(10), 
                DateTime.Today.AddDays(4).AddHours(10), 
                DateTime.Today.AddDays(5).AddHours(10), 
                DateTime.Today.AddDays(6).AddHours(10),

                DateTime.Today.AddDays(1).AddHours(12), 
                DateTime.Today.AddDays(2).AddHours(12), 
                DateTime.Today.AddDays(3).AddHours(12), 
                DateTime.Today.AddDays(4).AddHours(12), 
                DateTime.Today.AddDays(5).AddHours(12), 
                DateTime.Today.AddDays(6).AddHours(12), 
               
            };
            var durations = new TimeSpan[]
            {
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),          TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),          TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
            };
            var courses = await ApiWrapper.GetCourses();
            var course = courses.DataArray.Last();

            var group =
                await
                    ApiWrapper.CreateGroups(new string[] {"Test"}, new int[] {course.id},
                        new string[] {"de beschrijving van de group"});

            ApiWrapper.group
          //  var result = ApiWrapper.CreateCalanderEvents(course.Result.Data.)
              
//
//            var events = await ApiWrapper.GetCalanderEvents();
//            int[] eventids = new int[events.Data.events.Count];
//            int[] repeats = new int[eventids.Length];
//            for (int i = 0; i < events.Data.events.Count; i++)
//            {
//                eventids[i] = events.Data.events[i].id;
//                repeats[i] = 0;
//            }
//
//
//            var obj = await ApiWrapper.DeleteCalanderEvents(
//                eventids, repeats);
//              
//
            Console.WriteLine(ObjectDumper.Dump(course));
         }
    }

    public class ObjectDumper
    {
        private int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private readonly List<int> _hashListOfFoundElements;

        private ObjectDumper(int indentSize)
        {
            _indentSize = indentSize;
            _stringBuilder = new StringBuilder();
            _hashListOfFoundElements = new List<int>();
        }

        public static string Dump(object element)
        {
            return Dump(element, 2);
        }

        public static string Dump(object element, int indentSize)
        {
            var instance = new ObjectDumper(indentSize);
            return instance.DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    Write("{{{0}}}", objectType.FullName);
                    _hashListOfFoundElements.Add(element.GetHashCode());
                    _level++;
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            _level++;
                            DumpElement(item);
                            _level--;
                        }
                        else
                        {
                            if (!AlreadyTouched(item))
                                DumpElement(item);
                            else
                                Write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value = fieldInfo != null
                                           ? fieldInfo.GetValue(element)
                                           : propertyInfo.GetValue(element, null);

                        if (type.IsValueType || type == typeof(string))
                        {
                            Write("{0}: {1}", memberInfo.Name, FormatValue(value));
                        }
                        else
                        {
                            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                            Write("{0}: {1}", memberInfo.Name, isEnumerable ? "..." : "{ }");

                            var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                            _level++;
                            if (!alreadyTouched)
                                DumpElement(value);
                            else
                                Write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                            _level--;
                        }
                    }
                }

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    _level--;
                }
            }

            return _stringBuilder.ToString();
        }

        private bool AlreadyTouched(object value)
        {
            if (value == null)
                return false;

            var hash = value.GetHashCode();
            for (var i = 0; i < _hashListOfFoundElements.Count; i++)
            {
                if (_hashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * _indentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }

        private string FormatValue(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime)o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\"", o);

            if (o is char && (char)o == '\0')
                return string.Empty;

            if (o is ValueType)
                return (o.ToString());

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}
