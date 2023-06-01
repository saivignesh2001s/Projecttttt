using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BAL;
namespace DAL
{
    public class flightmethods
    {
        flightEntities context = null;
        public flightmethods() {
            context=new flightEntities();
        }
        public bool addmethod(flight p)
        {   
            try { 
              context.flights.Add(p);
              context.SaveChanges();
              return true;
            }
            catch {
                return false;
            }
        }
        public List<flight> flightlist() {
            return context.flights.ToList();
              
        
        
        }
    }
}
