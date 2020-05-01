//
// State Census Information
//

namespace program.Models
{

  public class StateCensus
	{
	
// data members with auto-generated getters and setters:
	  public string StateName { get; set; }
    public int Year {get;set;}
    public int Population {get; set;}
//    public string date { get; set; }
//		public int positive { get; set; }
//		public int negative { get; set; }
//		public int deaths { get; set; }
//		public string StationName { get; set; }
//		public int AvgDailyRidership { get; set; }
	
 // default constructor:
		public StateCensus()
		{ }

 // constructor:
		public StateCensus(string name, int yr, int popu)
		{
			StateName = name;   
      Year = yr;
			Population = popu;
		}
		
	}//end of class StateCensus

}//namespace