using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class Task3GraphModel : PageModel  
    {
        // Store the dates on which test data is recorded
        public List<string> datesTemp { get; set; }
        public List<string> dates { get; set; }
    
        public List<int> s1P { get; set; }
        public List<int> s1N { get; set; }
        public List<int> s2P { get; set; }
        public List<int> s2N { get; set; }
        public List<int> s3P { get; set; }
        public List<int> s3N { get; set; }
        
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        
        
        public Exception EX { get; set; }
  
        // When the page is loaded
        public void OnGet(string input1, string input2, string input3)  
        {
            datesTemp = new List<string>();
            
            s1P = new List<int>();
            s1N = new List<int>();
            s2P = new List<int>();
            s2N = new List<int>();
            s3P = new List<int>();
            s3N = new List<int>();            
            
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            
            EX = null;
          
            try
            {
                if (input1 == null || input2 == null || input3 == null)
                {
                    
                }
            
                else{
                    input1 = input1.Replace("'", "''");
                    input2 = input2.Replace("'", "''");
                    input3 = input3.Replace("'", "''");
                    // query retrieving data from US testing data table
                    string sql1 = string.Format(@"
                                SELECT date as DATE, positiveIncrease, negativeIncrease    
                                FROM us_states_covid19_daily
                                WHERE state LIKE '%{0}%'
                                ORDER BY date ASC
                                ", input1);

                    string sql2 = string.Format(@"
                                SELECT date as DATE, positiveIncrease, negativeIncrease   
                                FROM us_states_covid19_daily
                                WHERE state LIKE '%{0}%'
                                ORDER BY date ASC
                                ", input2);

                    string sql3 = string.Format(@"
                                SELECT date as DATE, positiveIncrease, negativeIncrease 
                                FROM us_states_covid19_daily
                                WHERE state LIKE '%{0}%'
                                ORDER BY date ASC
                                ", input3);
                
                    DataSet ds1 = DataAccessTier.DB.ExecuteNonScalarQuery(sql1);
                    DataSet ds2 = DataAccessTier.DB.ExecuteNonScalarQuery(sql2);
                    DataSet ds3 = DataAccessTier.DB.ExecuteNonScalarQuery(sql3);
                    
                    foreach (DataRow row in ds1.Tables[0].Rows){
                        string dateX = Convert.ToString(row["DATE"]);
                        datesTemp.Add(dateX);

                        int positiveIncreaseInt1 = Convert.ToInt32(row["positiveIncrease"]);
                        int negativeIncreaseInt1 = Convert.ToInt32(row["negativeIncrease"]);
                        s1P.Add(positiveIncreaseInt1);
                        s1N.Add(negativeIncreaseInt1);
                    }

                    foreach (DataRow row in ds2.Tables[0].Rows){
                        string dateX = Convert.ToString(row["DATE"]);
                        datesTemp.Add(dateX);

                        int positiveIncreaseInt2 = Convert.ToInt32(row["positiveIncrease"]);
                        int negativeIncreaseInt2 = Convert.ToInt32(row["negativeIncrease"]);
                        s2P.Add(positiveIncreaseInt2);
                        s2N.Add(negativeIncreaseInt2);
                    }

                    foreach (DataRow row in ds3.Tables[0].Rows){
                        string dateX = Convert.ToString(row["DATE"]);
                        datesTemp.Add(dateX);

                        int positiveIncreaseInt3 = Convert.ToInt32(row["positiveIncrease"]);
                        int negativeIncreaseInt3 = Convert.ToInt32(row["negativeIncrease"]);
                        s3P.Add(positiveIncreaseInt3);
                        s3N.Add(negativeIncreaseInt3);
                    }

                    dates = datesTemp.Distinct().ToList();  

                    while(s1P.Count()<dates.Count()){
                        s1P = s1P.Prepend(0).ToList();
                    }

                    while(s1N.Count()<dates.Count()){
                        s1N = s1N.Prepend(0).ToList();
                    }

                    while(s2P.Count()<dates.Count()){
                        s2P = s2P.Prepend(0).ToList();
                    }

                    while(s2N.Count()<dates.Count()){
                        s2N = s2N.Prepend(0).ToList();
                    }

                    while(s3P.Count()<dates.Count()){
                        s3P = s3P.Prepend(0).ToList();
                    }

                    while(s3N.Count()<dates.Count()){
                        s3N = s3N.Prepend(0).ToList();
                    }
                
                }
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