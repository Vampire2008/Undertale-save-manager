using System;
using Newtonsoft.Json;

namespace UndertaleSaveManager
{
	public class SaveModel
	{
		public string Name { get; set; }
		public string CharacterName { get; set; }
		public DateTime Date { get; set; }
		[JsonIgnore]
		public string Path { get; set; }
	}
}