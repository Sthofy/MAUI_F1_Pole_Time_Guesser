﻿namespace PoleTimeGuesser.Model
{
    public class GuessModel
    {
        public int Id { get; set; }
        public string Guess { get; set; } = null!;
        public string EventId { get; set; } = null!;
        public string DriverId { get; set; } = null!;
    }
}
