<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form1.aspx.cs" Inherits="CSE445ASN5.form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Enter your text to be fltered:</div>
        <asp:TextBox ID="TextBox1" runat="server" Height="58px" Width="281px"></asp:TextBox>
        <p>
            <asp:Button ID="Button1" runat="server" Height="42px" OnClick="Button1_Click" Text="Filter" Width="202px" />
        </p>
        <p>
            Filtered Text:</p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <p>
            <asp:Button ID="Button2" runat="server" Height="43px" OnClick="Button2_Click" Text="Definition of most used word" Width="214px" />
        </p>
        <p>
            This word will be saved in the cookies and its object will be cached.</p>
        Your word:
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <p>
            All definitions:
            <asp:Label ID="Label3" runat="server"></asp:Label>
        </p>
        Shortest Definition:
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <p>
&nbsp;</p>
        <asp:Button ID="Button3" runat="server" Height="37px" OnClick="Button3_Click" Text="Get synonyms of your word" Width="237px" />
        <br />
        Word:
        <asp:Label ID="Label6" runat="server"></asp:Label>
        <br />
        Synonyms:
        <asp:Label ID="Label7" runat="server"></asp:Label>
        <br />
        Antonyms:&nbsp;
        <asp:Label ID="Label8" runat="server"></asp:Label>
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="update cache" Width="222px" />
        (click twice to show cached filtered sentence)<p>
            Cached filtered text: <asp:Label ID="Label9" runat="server"></asp:Label>
        </p>
        <p>
            Last used word (In Cookie) =
            <asp:Label ID="Label5" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
