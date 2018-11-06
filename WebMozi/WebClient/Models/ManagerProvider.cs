using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebClient.Models
{
    public class ManagerProvider
    {
        private static ManagerProvider instance = null;
        public static ManagerProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new ManagerProvider();
                return instance;
            }
        }
        protected ManagerProvider() {
        }
        public  IReservationManager GetReservationManager() {
            return new RealReservationManager();
        }
        public ICinemaManager GetCinemaManager()
        {
            return new RealCinemaManager();
        }
    }
}
