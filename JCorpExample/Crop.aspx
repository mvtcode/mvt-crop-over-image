<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Crop.aspx.cs" Inherits="JCorpExample.Crop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/jquery.Jcrop.css" type="text/css" />
    <script src="js/jquery.Jcrop.min.js" type="text/javascript"></script>
    <script src="js/jquery.Jcrop.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        var sContentId = "ctl00_ContentPlaceHolder1_";
        jQuery(document).ready(function() {
            var iw = $('#imgFrame').width();
            var ih = $('#imgFrame').height();
            jQuery('#' + sContentId + 'W0').val(iw);
            jQuery('#' + sContentId + 'H0').val(ih);
            $('#outBox').width(iw);
            $('#outBox').height(ih);

            jQuery('#cropbox').Jcrop({
                onSelect: updateCoords,
                onChange: updateCoords,
                onRelease: hidePreview,
                bgColor: 'black',
                bgOpacity: .6,
                setSelect: [0, 0, iw, ih],
                aspectRatio: iw / ih
            });
            $('#Submit').click(function() {
            if ($('#' + sContentId + 'W').val() == '0') {
                    alert('Bạn hãy chọn vùng ảnh cần cắt');
                    return false;
                }
                return true;
            });
        });

        function updateCoords(c) {

            var rx = $('#outBox').width() / c.w;
            var ry = $('#outBox').height() / c.h;

            var w = Math.round(rx * $('#cropbox').width());
            var h = Math.round(ry * $('#cropbox').height());
            var x = Math.round(rx * c.x);
            var y = Math.round(ry * c.y);

            jQuery('#' + sContentId + 'X').val(x);
            jQuery('#' + sContentId + 'Y').val(y);
            jQuery('#' + sContentId + 'W').val(w);
            jQuery('#' + sContentId + 'H').val(h);
            
            $('#Img1').css({
                width: w + 'px',
                height: h + 'px',
                marginLeft: '-' + x + 'px',
                marginTop: '-' + y + 'px'
            });
        };
        function hidePreview() {
            $('#Img1').stop().fadeOut('fast');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>Xem trước:</div>
    <div style="position: relative; overflow: hidden; margin-left: 5px;" id="outBox">
        <img id="Img1" src='/images/anh.jpg' />
        <div style="position: absolute;top: 0px;left: 0px">
            <img src="/images/1.png" id="imgFrame" width="600"/>
        </div>
    </div>
    <div style="padding-top: 20px">
        <div>Cắt ảnh</div>
        <div>
            <img src="/images/anh.jpg" id="cropbox" width="600" />
        </div>
    </div>
    <br />
    <asp:Button ID="Submit" runat="server" Text="Corp Image" OnClick="Submit_Click" />
    <div style="display: none">
        <input type="hidden" id="X" runat="server" value="0" />
        <input type="hidden" id="Y" runat="server" value="0" />
        <input type="hidden" id="W" runat="server" value="0" />
        <input type="hidden" id="H" runat="server" value="0" />
        <input type="hidden" id="W0" runat="server" value="0" />
        <input type="hidden" id="H0" runat="server" value="0" />
    </div>
</asp:Content>
