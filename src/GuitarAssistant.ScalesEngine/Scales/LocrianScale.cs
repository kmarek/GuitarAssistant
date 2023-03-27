﻿namespace GuitarAssistant.ScalesEngine.Scales
{
    public class LocrianScale : IScale
    {
        public int NoteCount => 7;
        public List<int> Pattern => new() { 1, 2, 2, 1, 2, 2, 2 };
    }
}
