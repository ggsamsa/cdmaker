using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BackofficeServerLib
{
    //garante uma instancia do servico para todas as sessoes dos clientes
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)] 
    public class BackofficeService : IBackofficeService
    {
        private Order order;

        public BackofficeService()
        {
            order = new Order();
        }
        public string[] getTracksFromLastfm(string artist)
        {
            LastfmHelper lfm = new LastfmHelper();

            return lfm.getTracks(artist);
        }

        public string[] getSimilarNamed(string artist)
        {
            LastfmHelper lfm = new LastfmHelper();
            
            return lfm.getSimilarArtists(artist);
        }

        public void setArtistToOrder(string artist)
        {
            order.setArtist(artist);
        }

        public void addTrackToOrder(string title)
        {
            order.addTrack(title);
        }

        public float getTotalPrice()
        {
            return (float)order.getTotalPrice();
        }

        public List<string> getTracksFromOrder()
        {
            return order.getTracks();
        }

        public void setAddress(string address)
        {
            order.setAddress(address);
        }

        public void setClientUsername(string username)
        {
            order.clientUsername = username;
        }

        public void setOrderStatus(int idFab, string status)
        {
            MySingleton.Instance.setOrderStatus(idFab, status);
        }

        public List<string> getOrdersStatus(string username)
        {
            return MySingleton.Instance.getOrdersStatus(username);
        }

        public void commitOrder()
        {
            ProcessOrder proc = new ProcessOrder(order);
            proc.process();
        }
    }
}
