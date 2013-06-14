<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" href="./css/DnD.css" type="text/css" />

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome to My DnD "Character Sheet"</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:left; width:600px;">
            <div>
                <asp:Button ID="New3_5" runat="server" Text="New 3.5 Character" PostBackUrl="~/NewCharacter.aspx?edition=3_5" />
                <asp:Button ID="NewPathfinder" runat="server" Text="New Pathfinder Character" PostBackUrl="~/NewCharacter.aspx?edition=Pathfinder" />            
                <div class="clear"></div>
            </div>
            <asp:Repeater id="CharShortInfo" runat="server" OnItemDataBound="CharShortInfo_ItemDataBound">
                <ItemTemplate>
                    <asp:HyperLink ID="CharacterLink" runat="server">
                        <div class="CharacterPanel">
                            <asp:Label  runat="server" Width="250px" style="float:left"><%# Eval("Name")%></asp:Label >
                            <asp:Label  runat="server" Width="50px" style="float:left">| <%# Eval("Lvl")%></asp:Label >
                            <asp:Label  ID="RaceText" runat="server" Width="100px" style="float:left"/>
                            <asp:Label  ID="ClassText" runat="server" Width="100px" style="float:left"/>
                            <asp:Label  runat="server" Width="100px" style="float:left">| <%# Eval("Edition")%></asp:Label >
                            <div class="clear"></div>
                        </div>               
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
