//
// State Census Information
//

namespace program.Models
{

  public class ActionsTakenModel
	{
	
// data members with auto-generated getters and setters:
	  public string StateName { get; set; }
    public string Date {get;set;}
    public string News {get; set;}
	
 // default constructor:
		public ActionsTakenModel()
		{ }

 // constructor:
		public ActionsTakenModel(string name, string day, string desc)
		{
			StateName = name;   
      Date = day;
			News = desc;
		}
		
	}//end of class ActionsTakenModel

}//namespace