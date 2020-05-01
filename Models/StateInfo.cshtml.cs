using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace program.Pages  
{  
    public class StateInfoModel : PageModel  
    {  
        // Store census data about each state/year for the view to retrieve
				public List<Models.StateCensus> StateList { get; set; }
				public string Input { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet(string input)  
        {  
				  StateList = new List<Models.StateCensus>();
					
					// make input available to web page:
					Input = input;
					
					// clear exception:
					EX = null;
					
					try
					{
						//
						// Do we have an input argument?  If not, there's nothing to do:
						//
						if (input == null)
						{
							//
							// there's no page argument, perhaps user surfed to the page directly?  
							// In this case, nothing to do.
							//
						}
						else  
						{
							// 
							// Lookup census data based on input, which could be id or a partial name:
							// 
							string sql;

						  // lookup state by partial name match, sanitizing input for safety:
							input = input.Replace("'", "''");

							sql = string.Format(@"
                                  SELECT state, year, population
                                  FROM census
                                  WHERE state LIKE '%{0}%'
                                  ORDER BY Year;
                                  ", input);

							DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

							foreach (DataRow row in ds.Tables[0].Rows)
							{
								Models.StateCensus s = new Models.StateCensus();
                
                s.StateName = Convert.ToString(row["state"]);
								s.Year = Convert.ToInt32(row["year"]);
								s.Population = Convert.ToInt32(row["population"]);

								StateList.Add(s);
							}
						}//else
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
            // nothing at the moment, the data access tier cleans up after itself
				  }
				}
			
    }//class  
}//namespace