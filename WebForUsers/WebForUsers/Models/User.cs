using System;

namespace WebForUsers.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Registration { get; set; }

        public DateTime Authorization { get; set; }

        public int? StatusId { get; set; }

        public Status Status { get; set; }
    }
}
