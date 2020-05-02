using Newtonsoft.Json;
using RestSharp;
using System;

namespace Zoom.Models
{
    public partial class Meeting : IJsonObject
    {
        [JsonProperty("agenda")]
        public string Agenda { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("schedule_for")]
        public string ScheduleFor { get; set; }

        [JsonProperty("host_id")]
        public string HostId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("join_url")]
        public Uri JoinUrl { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("start_time")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("start_url")]
        public Uri StartUrl { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("type")]
        public @Type Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public partial class Settings
    {
        [JsonProperty("alternative_hosts")]
        public string AlternativeHosts { get; set; }

        [JsonProperty("approval_type")]
        public long ApprovalType { get; set; }

        [JsonProperty("audio")]
        public string Audio { get; set; }

        [JsonProperty("auto_recording")]
        public string AutoRecording { get; set; }

        [JsonProperty("close_registration")]
        public bool CloseRegistration { get; set; }

        [JsonProperty("cn_meeting")]
        public bool CnMeeting { get; set; }

        [JsonProperty("enforce_login")]
        public bool EnforceLogin { get; set; }

        [JsonProperty("enforce_login_domains")]
        public string EnforceLoginDomains { get; set; }

        [JsonProperty("global_dial_in_countries")]
        public string[] GlobalDialInCountries { get; set; }

        [JsonProperty("global_dial_in_numbers")]
        public GlobalDialInNumber[] GlobalDialInNumbers { get; set; }

        [JsonProperty("host_video")]
        public bool HostVideo { get; set; }

        [JsonProperty("in_meeting")]
        public bool InMeeting { get; set; }

        [JsonProperty("join_before_host")]
        public bool JoinBeforeHost { get; set; }

        [JsonProperty("mute_upon_entry")]
        public bool MuteUponEntry { get; set; }

        [JsonProperty("participant_video")]
        public bool ParticipantVideo { get; set; }

        [JsonProperty("registrants_confirmation_email")]
        public bool RegistrantsConfirmationEmail { get; set; }

        [JsonProperty("use_pmi")]
        public bool UsePmi { get; set; }

        [JsonProperty("waiting_room")]
        public bool WaitingRoom { get; set; }

        [JsonProperty("watermark")]
        public bool Watermark { get; set; }

        [JsonProperty("registrants_email_notification")]
        public bool RegistrantsEmailNotification { get; set; }
    }

    public partial class GlobalDialInNumber
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public enum @Type
    {
        Instant              = 1,
        Scheduled            = 2,
        RecurringNoFixedTime = 3,
        RecurringFixedTime   = 8
    }

    public partial class MeetingList : ZoomList
    {
        [JsonProperty("meetings")]
        public Meeting[] Meetings { get; set; }
    }

    public partial class Meeting
    {
        public static Meeting FromJson(string json) => JsonConvert.DeserializeObject<Meeting>(json, Converter.Settings);
        public static implicit operator Meeting(RestResponse response) => FromJson(response.Content);
    }

    public partial class MeetingList
    {
        public static MeetingList FromJson(string json) => JsonConvert.DeserializeObject<MeetingList>(json, Converter.Settings);
        public static implicit operator MeetingList(RestResponse response) => FromJson(response.Content);
        public static implicit operator Meeting[](MeetingList meetingList) => meetingList.Meetings;
    }

    //public static class MeetingExtensions
    //{
    //    public static Meeting ToMeeting(this IRestResponse response) => Meeting.FromJson(response.Content);
    //    public static Meeting[] ToMeetings(this IRestResponse response) => MeetingList.FromJson(response.Content).Meetings;
    //}
}
