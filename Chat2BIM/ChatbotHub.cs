using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Chat2BIM
{
    public class ChatbotHub : Hub
    {
        const string fileName = "C:\\Users\\Maik Gottfried\\source\\repos\\Chat2BIM\\Chat2BIM\\Assets\\LargeBuilding.ifc";


        public void Receive(string message)
        {
            string UserId = Context.ConnectionId;
            Clients.Client(UserId).ClientOwnReceive(DateTime.Now.TimeOfDay.ToString(@"hh\:mm"), message);
            System.Threading.Thread.Sleep(2000);
            Clients.Client(UserId).ClientReceive(DateTime.Now.TimeOfDay.ToString(@"hh\:mm"), "Nachricht empfangen: " + message);
        }

        public void getalldoors()
        {
            string UserId = Context.ConnectionId;
            using (var model = IfcStore.Open(fileName))
            {
                //get all walls in the model
                var walls = model.Instances.OfType<IIfcWall>();
                string output = "";

                //iterate over all the walls and change them
                foreach (var wall in walls)
                {
                    output += wall.Name + ": " + wall.GlobalId + ": " + wall.GetType().Name + "\n";

                }
                System.Diagnostics.Debug.WriteLine(output);
                Clients.Client(UserId).PrintOutput(output);
                //var properties = walls.First().IsDefinedBy.Where(r => r.RelatingPropertyDefinition is IIfcPropertySet).SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties).OfType<IIfcPropertySingleValue>();
                //foreach (var Property in properties)
                //{
                //    System.Diagnostics.Debug.WriteLine(Property.Name + ": " + Property.NominalValue);
                //}


            }
        }
        public void getallelements(string type)
        {
            string UserId = Context.ConnectionId;
            using (var model = IfcStore.Open(fileName))
            {
                var objs = model.Instances.OfType<IIfcBuildingElement>();
                Type objectType = Type.GetType(type);
                //System.Diagnostics.Debug.WriteLine(objectType.Name);
                /*PropertyInfo instancesProperty = model.GetType().GetProperty("Instances");
                MethodInfo ofTypeMethodDefinition = typeof(Enumerable).GetMethod("OfType");
                MethodInfo ofTypeMethod = ofTypeMethodDefinition.MakeGenericMethod(objectType);
                IEnumerable<object> objects = (IEnumerable<object>)ofTypeMethod.Invoke(null, new object[] { instancesProperty.GetValue(model) });
                */
                string output = "";
                
                foreach (var obj in objs)
                {
                    System.Diagnostics.Debug.WriteLine("Object #{0} has a type of {1}.", obj.EntityLabel, obj.GetType().Name);
                    if (obj.GetType().Name.Contains(TypeMapper.mapType(type))){
                        output += "Object #" + ((IIfcObject)obj).EntityLabel + " hat typ " + obj.GetType().Name + "\n";
                    }
                }
                
                Clients.Client(UserId).PrintOutput(output);
            }
        }
        public void getelementcount(string type)
        {
            string UserId = Context.ConnectionId;
            using (var model = IfcStore.Open(fileName))
            {
                var objs = model.Instances.OfType<IIfcBuildingElement>();
                Type objectType = Type.GetType(type);
                //System.Diagnostics.Debug.WriteLine(objectType.Name);
                /*PropertyInfo instancesProperty = model.GetType().GetProperty("Instances");
                MethodInfo ofTypeMethodDefinition = typeof(Enumerable).GetMethod("OfType");
                MethodInfo ofTypeMethod = ofTypeMethodDefinition.MakeGenericMethod(objectType);
                IEnumerable<object> objects = (IEnumerable<object>)ofTypeMethod.Invoke(null, new object[] { instancesProperty.GetValue(model) });
                */
                int count = 0;

                foreach (var obj in objs)
                {
                    System.Diagnostics.Debug.WriteLine("Object #{0} has a type of {1}.", obj.EntityLabel, obj.GetType().Name);
                    if (obj.GetType().Name.Contains(TypeMapper.mapType(type)))
                    {
                        count += 1;
                    }
                }

                Clients.Client(UserId).PrintOutput("Im Modell sind "+count+" Elemente vom Typ " + type + " vorhanden");
            }
        }
    }
}