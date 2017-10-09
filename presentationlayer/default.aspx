<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PresentationLayer._default" %>


<!DOCTYPE html>

<head runat="server">
<meta charset="UTF-8">
<title>DQF::Login</title>
<link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
<link href="vendor/bootstrap/css/bootstrap-select.min.css" rel="stylesheet">
<link href="vendor/fa/css/font-awesome.min.css" rel="stylesheet">
<meta name="description" content="First Advantage is a criminal background check company that delivers global solutions ranging from employment screenings to background checks for renters." /> 
<meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 
<meta http-equiv="pragma" content="no-cache" /> 
<meta http-equiv="content-style-type" content="text/css" /> 
<meta http-equiv="content-script-type" content="text/javascript" /> 

    
    <link href="css/main.css" rel="stylesheet" />
    <script type="text/javascript" src="../platform-api.sharethis.com/js/sharethis.js#property=58b871bbc15ccc00115b71b6&product=inline-share-buttons"></script>
    <script type="text/javascript" src="FADV_Assets/js/app.min.js"></script>
    <meta name="viewport" content="width=device-width" />



<!-- Custom styles for this template -->
<link href="css/main.css" rel="stylesheet">
</head>
<form runat ="server">
<body class="login LTR ENAU ContentBody">
   <script type="text/javascript" src="../secure.leadforensics.com/js/77795.js"></script>

<script src="WebResource5829.js?d=pynGkmcFUV13He1Qd6_TZLTgaCUhLkoe9lf73MYyz6ikT5YQY3KZfwZc1SsvgA9roJDHBF1xYeSUuJKV8gUqJQ2&amp;t=636160660665894255" type="text/javascript"></script>

<input type="hidden" name="lng" id="lng" value="en-AU" />
    <div id="ctl00_ctxM">

</div>
       <div>

      </div>
    <%--<div class="primary-nav-wrapper hide-for-medium-down">

</div>--%>
    

          

<div class="login_wrapper">
  

  <div class="login-box">
    <div class="login-logo"> <img src="Admin/Images/logo_header.png" width="250" height="100" alt="" /> </div>
    <!-- /.login-logo -->
    <div class="login-box-body">
      <p class="login-box-msg">Sign in to start your session</p>
        
       <asp:ValidationSummary 
            ID="ValidationSummary1" 
            runat="server" 
            HeaderText="Following error occurs....." 
            ShowMessageBox="false" 
            DisplayMode="BulletList" 
            ShowSummary="true"
            BackColor="Snow"
           ValidationGroup="forgotpass"
            Width="400"
            ForeColor="Red"
            Font-Size="Small"
          
            Font-Italic="true"
           
            />

        <div class="form-group has-feedback">
  <asp:TextBox ID="txtAgentID" runat="server" placeholder="Username" class="form-control" maxlength="80" size="30" ValidationGroup="forgotpass" ></asp:TextBox>
               <asp:RequiredFieldValidator
             ID="RequiredFieldValidator1"
             runat="server"
                   
             ControlToValidate="txtAgentID"
             ErrorMessage='Input your Email'
             EnableClientScript="true"
             SetFocusOnError="true" ForeColor="White"
                   
                   ValidationGroup="forgotpass"
             Text=""
             >
        </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtAgentID"
                                            ErrorMessage="Input valid email address!" ValidationGroup="forgotpass" ForeColor="White" EnableClientScript="true"
             SetFocusOnError="true" Text="" Display="Dynamic"> </asp:RegularExpressionValidator>
            

          <span class="fa fa-user form-control-feedback"></span> <span><font color="red"></font></span> </div>
        <div class="form-group has-feedback">
              <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"  placeholder="Password" maxlength="80" size="30" ValidationGroup="forgotpass" /></asp:TextBox>
                           <asp:RequiredFieldValidator
             ID="RequiredFieldValidator2"
             runat="server"
             ControlToValidate="txtPassword"
             ErrorMessage='Input your Password'
                               ValidationGroup="forgotpass"
                               ForeColor="White"
             EnableClientScript="true"
             SetFocusOnError="true"
             Text="*"
             >
                               <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label>
        </asp:RequiredFieldValidator>
          <span class="fa fa-lock form-control-feedback"></span> <span><font color="red"></font></span> </div>
          
           <div class="icheckbox_square-blue" aria-checked="false" aria-disabled="false">
               <%-- <input type="checkbox" runat="server" name="remember" value="" id="chkRemember"/>   Remember me--%>
               <asp:CheckBox runat="server" ID="chkRemember"/> Remember me
                </div>
     
        
        <div class="row">
            <div class="col col-12 text-right">
        	 <asp:Button ID="btnLogin" OnClientClick="RemoveErrorMessage();" ValidationGroup="forgotpass" class="mq-btn btn btn-primary nxt_btn" runat="server" Text="Sign In" OnClick="btnLogin_Click"/>
           </div>
            </div>
    
        
      
      <a href="ForgotPassWord.aspx">I forgot my password</a><br>
       </div>
    <!-- /.login-box-body --> 
      
  </div>
</div>

<script src="vendor/jquery/jquery.js"></script> 
<script src="vendor/bootstrap/js/bootstrap.min.js"></script> 
<script src="vendor/bootstrap/js/bootstrap-select.min.js"></script>

<script type="text/javascript">
     (function   {
         "use strict";

         // Options for Message
         //----------------------------------------------
         var options = {
             'btn-loading': '<i class="fa fa-spinner fa-pulse"></i>',
             'btn-success': '<i class="fa fa-check"></i>',
             'btn-error': '<i class="fa fa-remove"></i>',
             'msg-success': 'All Good! Redirecting...',
             'msg-error': 'Wrong login credentials!',
             'useAJAX': true,
         };

         // Login Form
         //----------------------------------------------
         // Validation
         $("#login-form").validate({
             rules: {
                 lg_username: "required",
                 lg_password: "required",
             },
             errorClass: "form-invalid"
         });

         // Form Submission
         $("#login-form").submit(function () {
             remove_loading($(this));

             if (options['useAJAX'] == true) {
                 $.ajax({
                     type: "Post",
                     url: "index.aspx/validateUser",
                     success: function (response) {
                         data: '{UserName: "test" ,Password: "test",test:"yes"}';
                         contentType: "application/json; charset=utf-8";
                         dataType: "json";
                     }
                 });

                 dummy_submit_form($(this));

                 // Cancel the normal submission.
                 // If you don't want to use AJAX, remove this
                 return false;
             }
         });

         // Register Form
         //----------------------------------------------
         // Validation
         $("#register-form").validate({
             rules: {
                 reg_username: "required",
                 reg_password: {
                     required: true,
                     minlength: 5
                 },
                 reg_password_confirm: {
                     required: true,
                     minlength: 5,
                     equalTo: "#register-form [name=reg_password]"
                 },
                 reg_email: {
                     required: true,
                     email: true
                 },
                 reg_agree: "required",
             },
             errorClass: "form-invalid",
             errorPlacement: function (label, element) {
                 if (element.attr("type") === "checkbox" || element.attr("type") === "radio") {
                     element.parent().append(label); // this would append the label after all your checkboxes/labels (so the error-label will be the last element in <div class="controls"> )
                 }
                 else {
                     label.insertAfter(element); // standard behaviour
                 }
             }
         });

         // Form Submission
         $("#register-form").submit(function () {
             remove_loading($(this));

             if (options['useAJAX'] == true) {
               
               //  dummy_submit_form($(this));

                 // Cancel the normal submission.
                 // If you don't want to use AJAX, remove this
                 return false;
             }
         });

         // Forgot Password Form
         //----------------------------------------------
         // Validation
         $("#forgot-password-form").validate({
             rules: {
                 fp_email: "required",
             },
             errorClass: "form-invalid"
         });

         // Form Submission
         $("#forgot-password-form").submit(function () {
             remove_loading($(this));

             if (options['useAJAX'] == true) {
                 // Dummy AJAX request (Replace this with your AJAX code)
                 // If you don't want to use AJAX, remove this
                 dummy_submit_form($(this));

                 // Cancel the normal submission.
                 // If you don't want to use AJAX, remove this
                 return false;
             }
         });

         // Loading
         //----------------------------------------------
         function remove_loading($form) {
             $form.find('[type=submit]').removeClass('error success');
             $form.find('.login-form-main-message').removeClass('show error success').html('');
         }

         function form_loading($form) {
             $form.find('[type=submit]').addClass('clicked').html(options['btn-loading']);
         }

         function form_success($form) {
             $form.find('[type=submit]').addClass('success').html(options['btn-success']);
             $form.find('.login-form-main-message').addClass('show success').html(options['msg-success']);
         }

         function form_failed($form) {
             $form.find('[type=submit]').addClass('error').html(options['btn-error']);
             $form.find('.login-form-main-message').addClass('show error').html(options['msg-error']);
         }

         // Dummy Submit Form (Remove this)
         //----------------------------------------------
         // This is just a dummy form submission. You should use your AJAX function or remove this function if you are not using AJAX.
         function dummy_submit_form($form) {
             if ($form.valid()) {
                 form_loading($form);

                 setTimeout(function () {
                     form_success($form);
                 }, 2000);
             }
         }
     })(jQuery);
 </script>
</body>
    </form>



</html>