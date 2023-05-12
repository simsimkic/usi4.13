using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Notification : Identifier, ISerializable
    {
        public override object Key { get => Id; set => Id = (int)value; }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; }
        public string[] Recipients { get; set; }

        public Notification() : this("", new string[0]) { }
        public Notification(string text, params string[] recipients) :
            this(0, text, false, recipients) { }
        public Notification(int id, string text, bool seen, params string[] recipients)
        {
            Id = id;
            Text = text;
            Seen = seen;
            Recipients = recipients;
        }

        public string Display()
        {
            Seen = true;
            return Text;
        }

        public string[] ToCSV()
        {
            string recipients = Utility.ToString(Recipients);
            return new string[] { Id.ToString(), recipients, Text, Seen.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Recipients = values[1].Split("|");
            Text = values[2];
            Seen = bool.Parse(values[3]);
        }
    }
}
