using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M16_FPP_5_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Technician> technicianList = TechnicianDB.GetTechnicians();
            List<Incident> incidentList = IncidentDB.GetIncidents();

            var incidents = from incident in incidentList
                            join technician in technicianList
                            on incident.TechID equals technician.TechID
                            orderby technician.Name, incident.DateOpened ascending
                            select new
                            {
                                technician.Name,
                                incident.ProductCode,
                                incident.DateOpened,
                                incident.DateClosed,
                                incident.Title
                            };

            string tecnicianName = "";
            int i = 0;
            foreach (var incident in incidents)
            {
                if (incident.DateClosed != null)
                {
                    if (incident.Name != tecnicianName)
                    {
                        lvIncidents.Items.Add(incident.Name);
                        tecnicianName = incident.Name;
                    }
                    else
                    {
                        lvIncidents.Items.Add("");
                    }
                    lvIncidents.Items[i].SubItems.Add(incident.ProductCode);
                    lvIncidents.Items[i].SubItems.Add(
                        Convert.ToDateTime(incident.DateOpened).ToShortDateString());
                    lvIncidents.Items[i].SubItems.Add(
                        Convert.ToDateTime(incident.DateClosed).ToShortDateString());
                    lvIncidents.Items[i].SubItems.Add(incident.Title);
                    i += 1;
                }

            }
        }
    }
}
