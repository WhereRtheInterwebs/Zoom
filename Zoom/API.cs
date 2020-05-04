using RestSharp;
using System;
using Zoom.Models;
using System.Linq;

namespace Zoom
{
    public static class API
    {
        public const string BaseApiTarget = "https://api.zoom.us/v2";

        public static Meeting GetMeeting(long MeetingID) =>
            ZoomRequest($"/meetings/{MeetingID}");

        public static Meeting[] GetMeetingsByUser(string UserID) => 
            (MeetingList)ZoomRequest($"/users/{UserID}/meetings");

        public static Meeting CreateMeeting(string UserID, string Topic, Models.Type Type, DateTime StartTime, int DurationMinutes)
        {
            var meeting = new Meeting()
            {
                Topic       = Topic,
                Type        = Type,
                StartTime   = StartTime,
                Duration    = DurationMinutes,
                ScheduleFor = UserID,
                Password    = "34567789",
                Timezone    = "America/Chicago"
            };

            return ZoomRequest($"/users/{UserID}/meetings", Method.POST, meeting);
        }

        public static Meeting DeleteMeeting(long MeetingID) => 
            ZoomRequest($"/meetings/{MeetingID}", Method.DELETE);

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
