using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.BLL
{
    public class ManageBase
    {
        public virtual  void CreateObject(ConnectionInfo connInfo, string nodeName)
        {
            throw new NotImplementedException();
        }

        public virtual string ShowScript(string dbName, ConnectionInfo connInfo, string name)
        {
            throw new NotImplementedException();
        }

        public virtual void RenameObject(ConnectionInfo connInfo, string oldName, string newName, string parentNodeName)
        {
            throw new NotImplementedException();
        }

        public virtual void RenameObject(ConnectionInfo connInfo, string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteObject(ConnectionInfo connInfo, string nodeName)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteObject(ConnectionInfo connInfo, string nodeType, string parentNodeName, string nodeName)
        {
            throw new NotImplementedException();
        }
    }
}
