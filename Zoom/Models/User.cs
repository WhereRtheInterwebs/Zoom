using System;
using Newtonsoft.Json;
using RestSharp;

namespace Zoom.Models
{
    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("type")]
        public UserType Type { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [JsonProperty("pmi")]
        public long Pmi { get; set; }

        [JsonProperty("use_pmi")]
        public bool UsePmi { get; set; }

        [JsonProperty("personal_meeting_url")]
        public Uri PersonalMeetingUrl { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("verified")]
        public long Verified { get; set; }

        [JsonProperty("dept")]
        public string Dept { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("last_login_time")]
        public DateTimeOffset LastLoginTime { get; set; }

        [JsonProperty("last_client_version")]
        public string LastClientVersion { get; set; }

        [JsonProperty("pic_url")]
        public Uri PicUrl { get; set; }

        [JsonProperty("host_key")]
        public string HostKey { get; set; }

        [JsonProperty("jid")]
        public string Jid { get; set; }

        [JsonProperty("group_ids")]
        public object[] GroupIds { get; set; }

        [JsonProperty("im_group_ids")]
        public string[] ImGroupIds { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("phone_country")]
        public string PhoneCountry { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public enum UserType
    {
        Basic = 1,
        Licensed = 2,
        OnPrem = 3
    }

    public partial class User
    {
        public static User FromJson(string json) => JsonConvert.DeserializeObject<User>(json, Converter.Settings);
        public static implicit operator User(RestResponse response) => FromJson(response.Content);
    }
    
    public partial class UserList : ZoomList
    {
        [JsonProperty("users")]
        public User[] Users { get; set; }
    }

    public partial class UserList
    {
        public static UserList FromJson(string json) => JsonConvert.DeserializeObject<UserList>(json, Converter.Settings);
        public static implicit operator UserList(RestResponse response) => FromJson(response.Content);
        public static implicit operator User[] (UserList userList) => userList.Users;
    }
}