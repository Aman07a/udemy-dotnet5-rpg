﻿namespace udemy_dotnet5_rpg.Models
{
	public class ServiceResponse<T>
	{
		public T Data { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}