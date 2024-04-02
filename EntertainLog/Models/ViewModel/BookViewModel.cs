﻿namespace EntertainLog.Models.ViewModel
{
    public class BookViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public Book NewBook { get; set; }
        public User Curruser { get; }

        public Book CurrBook { get; set; }
    }
}
