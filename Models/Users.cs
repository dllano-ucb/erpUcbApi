using System;

namespace erpUcbApi.Models {
    public class Users {
        public int Id {get; set;}
        public string Username {get; set;}
        public string PasswordHash {get; set;}
        public string Email {get; set;}
        public string Status {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}