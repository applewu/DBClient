<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Jxmstc.Sop.DbClient.Web.UserControl.List" %>

<asp:Panel ID="Panel1" runat="server" Height="424px" Width="251px">
            <div id="CategoryTree" class="TreeMenu"></div>
        </asp:Panel>
        <script language="javascript" >
            var tree = document.getElementById("CategoryTree");
            var root = document.createElement("li");
            root.id = "li_0";
            tree.appendChild( root );
            ExpandSubCategory( 0 );
            function ExpandSubCategory( categoryID )
            {
                var liFather = document.getElementById( "li_" + categoryID );
                if( liFather.getElementsByTagName("li").length > 0)
                {
                    ChangeStatus( categoryID );
                    return;
                }
                liFather.className = "Opened";
                SwitchNode( categoryID, true );
                
                //仅获取当前节点的子Nodes
                Jxmstc.Sop.DbClient.Web.UserControl.List.GetSubCategory( categoryID, GetSubCategory_callback );
            }            
            function SwitchNode( CategoryID, show )
            {
                var li_father = document.getElementById("li_" + CategoryID);
                if( show )
                {
                    var ul = document.createElement("ul");
                    ul.id = "ul_note_" + CategoryID;
                    
                    var note = document.createElement("li");
                    note.className = "Child";              
                    
                    var img = document.createElement("img");
                    img.className = "s";
                    img.src = "css/s.gif";                    
                    
                    var a = document.createElement("a");
                    a.href = "javascript:void(0);";
                    a.innerHTML = "Please waiting...";
                    
                    note.appendChild(img);
                    note.appendChild(a);
                    ul.appendChild(note);
                    li_father.appendChild(ul);                                        
                }   
                else
                {
                    var ul = document.getElementById("ul_note_" + CategoryID );
                    if( ul )
                    {
                        li_father.removeChild(ul);
                    }
                }             
            }
            function GetSubCategory_callback( response )
            {
               var dt = response.value.Tables[0];
               if( dt.Rows.length > 0 )
               {
                    var iCategoryID = dt.Rows[0].FatherID;               
               }                                
               var li_father = document.getElementById("li_" + iCategoryID );
               var ul = document.createElement("ul");
               for( var i = 0; i < dt.Rows.length; i++ )
               {
                    if( dt.Rows[i].IsChild == 1 )
                    {
                        var li = document.createElement("li");
                        li.className = "Child";
                        li.id = "li_" + dt.Rows[i].CategoryID;
                        var img = document.createElement("img");
                        img.id = dt.Rows[i].CategoryID;
                        img.className = "s";
                        img.src = "css/s.gif";
                        var a = document.createElement("a");
                        a.href = "javascript:OpenDocument('" + dt.Rows[i].CategoryID + "');";
                        a.innerHTML = dt.Rows[i].CategoryName;                                          
                    }
                    else
                    {
                        var li = document.createElement("li");
                        li.className = "Closed";
                        li.id = "li_" + dt.Rows[i].CategoryID;
                        var img = document.createElement("img");
                        img.id = dt.Rows[i].CategoryID;
                        img.className = "s";
                        img.src = "css/s.gif";
                        img.onclick = function(){ ExpandSubCategory( this.id ); };
                        img.alt = "Expand/collapse";
                        var a = document.createElement("a");
                        a.href = "javascript:ExpandSubCategory('" + dt.Rows[i].CategoryID + "');";
                        a.innerHTML = dt.Rows[i].CategoryName;                                         
                    }
                    li.appendChild(img);
                    li.appendChild(a);
                    ul.appendChild(li);
               }
               li_father.appendChild(ul);
               SwitchNode( iCategoryID, false );
            }          
            
            //单击叶节点时, 异步从服务端获取单个节点的数据.
            function OpenDocument( CategoryID )
            {                
                List.GetNameByCategoryID( CategoryID, GetNameByCategoryID_callback );
            }
            
            function GetNameByCategoryID_callback( response )
            {
                alert( response.value );
            }
            
            function ChangeStatus( CategoryID )
            {
                var li_father = document.getElementById("li_" + CategoryID );
                if( li_father.className == "Closed" )
                {
                    li_father.className = "Opened";
                }
                else
                {
                    li_father.className = "Closed";
                }
           }              
        </script>        