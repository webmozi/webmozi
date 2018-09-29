using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebClient.Models
{
    public class ReservationManagerProvider
    {
        private static ReservationManagerProvider instance = null;        public static ReservationManagerProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReservationManagerProvider();
                return instance;
            }
        }
        protected ReservationManagerProvider() {
        }        public  IReservationManager GetReservationManager() {
            return new MockReservationManager();
        }
    }
}
