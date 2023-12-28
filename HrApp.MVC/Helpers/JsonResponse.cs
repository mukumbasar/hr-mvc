﻿namespace HrApp.MVC.Helpers
{
    public class JsonResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}