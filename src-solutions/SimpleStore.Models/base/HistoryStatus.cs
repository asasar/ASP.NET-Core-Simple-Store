namespace SimpleStore.Models
{
    public class HistoryStatus
    {

        public int Id { get; set; }

        public System.DateTime Created { get; set; }

        public StatusInformation StatusOrder { get; set; }
    }
}