﻿namespace Models.ModelsVM.Response.TripResponse
{
    public class ReviewResponse
    {
        public int TripId { get; set; }
        public string? UserName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
