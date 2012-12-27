<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JCorpExample._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.Jcrop.min.js"></script>
    <script src="js/jquery.Jcrop.js"></script>
    <link rel="stylesheet" href="css/jquery.Jcrop.css" type="text/css" />
    <script language="Javascript" type="text/javascript">
        jQuery(document).ready(function() {
            var iw = $('#imgFrame').width();
            var ih = $('#imgFrame').height();
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
                if ($('#W').val() == '0') {
                    alert('Bạn hãy chọn vùng ảnh cần cắt');
                    return false;
                }
                return true;
            });
        });

        function updateCoords(c) {
            jQuery('#X').val(c.x);
            jQuery('#Y').val(c.y);
            jQuery('#W').val(c.w);
            jQuery('#H').val(c.h);

            var rx = $('#outBox').width() / c.w;
            var ry = $('#outBox').height() / c.h;
            //$('#Img1').attr('src', 'Sunset.jpg');
            $('#Img1').css({
                width: Math.round(rx * $('#cropbox').width()) + 'px',
                height: Math.round(ry * $('#cropbox').height()) + 'px',
                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                marginTop: '-' + Math.round(ry * c.y) + 'px'
            });
        };
        function hidePreview() {
            $('#Img1').stop().fadeOut('fast');
        }
	</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="position: relative; overflow: hidden; margin-left: 5px;" id="outBox">
            <img id="Img1" src='/images/anh.jpg' />
            <div style="position: absolute;top: 0px;left: 0px">
                <img src="/images/1.png" id="imgFrame" width="400"/>
            </div>
        </div>
        <img src="/images/anh.jpg" id="cropbox" width="400" />
        <br />
        <asp:Button ID="Submit" runat="server" Text="Corp Image" OnClick="Submit_Click" />
        <input type="hidden" id="X" runat="server" value="0" />
        <input type="hidden" id="Y" runat="server" value="0" />
        <input type="hidden" id="W" runat="server" value="0" />
        <input type="hidden" id="H" runat="server" value="0" />
    </div>
    </form>
</body>
</html>
