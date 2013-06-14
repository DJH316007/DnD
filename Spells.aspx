<%@ Page Language="C#" MasterPageFile="~/DnD.master" AutoEventWireup="true" CodeFile="Spells.aspx.cs" Inherits="Spells" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TabSwitch" Runat="Server">
    <link rel="stylesheet" href="./css/Spells.css" type="text/css" />

    <script type="text/javascript">
        function SpellClick(t){
		        document.getElementById('<%=SpellDescriptText.ClientID %>').value = t;
        }
    </script>

    <div>
        <asp:Button ID="Spells0Button" runat="server" Text="Level 0" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells1Button" runat="server" Text="Level 1" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells2Button" runat="server" Text="Level 2" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells3Button" runat="server" Text="Level 3" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells4Button" runat="server" Text="Level 4" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells5Button" runat="server" Text="Level 5" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells6Button" runat="server" Text="Level 6" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells7Button" runat="server" Text="Level 7" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells8Button" runat="server" Text="Level 8" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />
        <asp:Button ID="Spells9Button" runat="server" Text="Level 9" CssClass="SpellLevelButton" OnClick="SpellLevel_Click" />       
    </div>

    <div style="float:left; border: 1px #000000 solid;">
        <div class="SpellAttributeMdHeader" style="float:left; text-align:center;">Name</div>
        <div class="SpellAttributeMdHeader" style="float:left; text-align:center;">Casting Time</div>
        <div class="SpellAttributeMdHeader" style="float:left; text-align:center;">Duration</div>
        <div class="SpellAttributeMdHeader" style="float:left; text-align:center;">Range</div>
        <div class="SpellAttributeMdHeader" style="float:left; text-align:center;">Save</div>
        <div class="SpellAttributeSmHeader" style="float:left; text-align:center;">SR</div>
        <div style="clear:both"></div>
                        
        <div class="SpellAttributeArea">
            <asp:Repeater id="SpellTable" runat="server">
                <HeaderTemplate></HeaderTemplate>                
                <ItemTemplate>
                    <div onclick="SpellClick('<%#Eval("Description")%>');">
                        <div class="SpellAttributeMd" style="float:left; text-align:center;"><%#Eval("Name")%></div>
                        <div class="SpellAttributeMd" style="float:left; text-align:center;"><%#Eval("Time")%></div>
                        <div class="SpellAttributeMd" style="float:left; text-align:center;"><%#Eval("Duration")%></div>
                        <div class="SpellAttributeMd" style="float:left; text-align:center;"><%#Eval("Range")%></div>
                        <div class="SpellAttributeMd" style="float:left; text-align:center;"><%#Eval("Saving")%></div>
                        <div class="SpellAttributeSm" style="float:left; text-align:center;"><%#Eval("SpellRes")%></div>     
                    </div>
                </ItemTemplate>
            </asp:Repeater>    
        </div>
    </div>

    <div style="float:left;">   
        <div class="SPDHeader">Class Spells Per Day</div>
        <div class="SPDLevel">Level 0 (<%= SpellsPerDay(0) %>)</div>
        <div class="SPDLevel">Level 1</div>
        <div class="SPDLevel">Level 2</div>
        <div class="SPDLevel">Level 3</div>
        <div class="SPDLevel">Level 4</div>
        <div class="SPDLevel">Level 5</div>
        <div class="SPDLevel">Level 6</div>
        <div class="SPDLevel">Level 7</div>
        <div class="SPDLevel">Level 8</div>
        <div class="SPDLevel">Level 9</div>

        <asp:Repeater id="SpellsPerDayyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>                
            <ItemTemplate>
                <div class="SPDHeader">Class Spells Per Day</div>
                <div class="SPDLevel">Level 0 (<%= SpellsPerDay(0) %>)</div>
                <div class="SPDLevel">Level 1</div>
                <div class="SPDLevel">Level 2</div>
                <div class="SPDLevel">Level 3</div>
                <div class="SPDLevel">Level 4</div>
                <div class="SPDLevel">Level 5</div>
                <div class="SPDLevel">Level 6</div>
                <div class="SPDLevel">Level 7</div>
                <div class="SPDLevel">Level 8</div>
                <div class="SPDLevel">Level 9</div>
            </ItemTemplate>
        </asp:Repeater>    
    </div>    
    <div style="clear:both"></div>
    <asp:TextBox ID="SpellDescriptText" runat=server Width="1185px" Height="200px" TextMode=MultiLine/>
</asp:Content>

