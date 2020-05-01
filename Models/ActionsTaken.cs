using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace program.Pages  
{  
    public class ActionsTaken : PageModel  
    {  
				public List<Models.ActionsTakenModel> NewsList { get; set; }
				public string Input { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet()  
        {  
				  NewsList = new List<Models.ActionsTakenModel>();

					// clear exception:
					EX = null;
					
					try
					{
							string sql;

							sql = string.Format(@"
SELECT Country, StartDate, DescriptionOfMeasure 
FROM covid_mitigation_usa
ORDER BY StartDate DESC;
");

							DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

							foreach (DataRow row in ds.Tables[0].Rows)
							{
								Models.ActionsTakenModel s = new Models.ActionsTakenModel();
                
                s.StateName = Convert.ToString(row["Country"]);
								s.Date = Convert.ToString(row["StartDate"]);
								s.News = Convert.ToString(row["DescriptionOfMeasure"]);

								NewsList.Add(s);
							}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
					  // nothing at the moment
				  }
				}
			
    }//class  
}//namespace