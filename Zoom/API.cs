using RestSharp;
using System;
using Zoom.Models;
using System.Linq;

namespace Zoom
{
    public static class API
    {
        public const string BaseApiTarget = "https://api.zoom.us/v2";

        #region "Meetings"
        public static Meeting GetMeeting(long MeetingID) =>
            ZoomRequest($"/meetings/{MeetingID}");

        public static Meeting[] GetMeetingsByUser(string UserID) => 
            (MeetingList)ZoomRequest($"/users/{UserID}/meetings");

        public static Meeting CreateMeeting(string UserID, string Topic, MeetingType Type, DateTime StartTime, int DurationMinutes, Settings Settings = null)
        {
            string password = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                double flt = random.NextDouble();
                password += Math.Floor(flt * 10).ToString();
            }

            var meeting = new Meeting()
            {
                Topic       = Topic,
                Type        = Type,
                StartTime   = StartTime,
                Duration    = DurationMinutes,
                ScheduleFor = UserID,
                Password    = password,
                Timezone    = "America/Chicago",
                Settings    = Settings
            };
            
            var response = ZoomRequest($"/users/{UserID}/meetings", Method.POST, meeting);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Ambiguous)
                throw new Exception("Maximum number of meetings have been scheduled for this user today");
            else if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                if (GetUser(UserID).Type == UserType.Licensed)
                {
                    var alternateHosts = Settings.AlternativeHosts?.Split(',').Where(x => GetUser(x).Type == UserType.Basic);
                    if (alternateHosts.Any())
                        throw new Exception("The following Alternate Hosts are not licenesed users.  This is not allowed:  " + String.Join(", ", alternateHosts));
                }
                
                throw new Exception("Unable to create Zoom meeting.  " + response.ErrorMessage);
            }

            return response;
        }

        public static RestResponse UpdateMeeting(long MeetingID, Meeting Meeting) =>
            ZoomRequest($"/meetings/{MeetingID}", Method.PATCH, Meeting);

        public static Meeting DeleteMeeting(long MeetingID) => 
            ZoomRequest($"/meetings/{MeetingID}", Method.DELETE);
        #endregion

        #region "Users"
        public static User GetUser(string UserEmail) =>
            ZoomRequest($"/users/{UserEmail}");
        #endregion

        public static RestResponse ZoomRequest(string ApiTarget, Method Method = Method.GET, IJsonObject Body = null)
        {
            var client = new RestClient(BaseApiTarget + ApiTarget);
            var request = new RestRequest(Method);
            request.AddHeader("authorization", "Bearer " + new Token());

            if (Body != null)
                request.AddParameter("application/json", Body.ToJson(), ParameterType.RequestBody);
            
            return (RestResponse)client.Execute(request);
        }
    }
}
