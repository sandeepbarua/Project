<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PresentationLayer.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
	Dashboard
</title>
    <meta name="description" content="XChange Advantage REST API provides a robust, expedient, and simple API for interacting with First Advantage Screening Services using RESTful transactions. " /> 
<meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 
<meta http-equiv="pragma" content="no-cache" /> 
<meta http-equiv="content-style-type" content="text/css" /> 
<meta http-equiv="content-script-type" content="text/javascript" /> 
 
<link href="../favicon.ico" type="image/x-icon" rel="shortcut icon"/> 
<link href="../favicon.ico" type="image/x-icon" rel="icon"/> 
    <link href="FADV_Assets/css/app.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../platform-api.sharethis.com/js/sharethis.js#property=58b871bbc15ccc00115b71b6&product=inline-share-buttons"></script>
    <script type="text/javascript" src="../FADV_Assets/js/app.min.js"></script>
    <meta name="viewport" content="width=device-width" />
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    
   
   
</head>
<body class="LTR ENAU ContentBody" >

    <form runat="server">






<div id="ctl00_ctxM">

</div>
 
    <div class="MainDiv">
        <!-- logo and header -->
        
        
        <div class="MainContent">
            <!-- main content -->
            
    
            <!-- Container -->
            <div>
               <header>
  <!--Secondary Header Markup-->
  <div class="secondary-header hide-for-small">
    <div class="row">
      <div class="columns">
        <nav class="language">
          <a class="nav-category international">
          <img src="../Fadv-prod/media/Assets/Logos/Corporate/FADV-Logo-Background-Check-Services.png" height="80" width="150"/>
          </a>
        </nav>
      </div>
      <div>
        <ul class="utility-nav">
          
          <li>
            <a href="../customers.html">
              Customers
            </a>
          </li>
          
          <li>
            <a href="../candidates.html">
              Candidates
            </a>
          </li>
          
          <li>
            <a href="../contact.html">
              Contact
            </a>
          </li>
          
          <li>
            <a href="#">
              Call <tel:02.9017.4300> (02) 9017 4300
            </a>
          </li>
          
          <li class="search">
            <!--Include Search Form-->
            <div class="fadv-search" id="fadv_search">
                <input class="fadv-search-input" placeholder="Enter your search term..." type="text" value="" name="search" id="search">
                </input>
                <input class="fadv-search-submit" type="submit" value="">
                </input>
                <i class="icon-search fadv-icon-search"></i>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
  <!--END Secondary Header Markup-->
</header>


 <!--Primary Nav Markup-->
<div class="primary-nav-wrapper hide-for-medium-down">
  <div class="row primary-nav">
    <div class="columns">
      <div class="fadv-logo">
        <a href="../index.html">
        
        </a>
      </div>
    </div>
    <div>
    </div>
  </div>
</div>
<!--END Primary Nav Markup-->
<!--Mega Menu markup-->
<div class="menu">
  
  <div class="columns menu-content solutions">
    <div class="row">
      <div class="columns">
        <ul>
          <li>
            <a class="overview" href="../solutions.html">
            Solutions Overview
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row">
      
      <div class="sub-menu-content columns">
        <i class="icon-industry"></i>
        <h3>
          By Industry
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../solutions/financial-background-checks.html">
              Financial Services
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/healthcare-background-checks.html">
              Healthcare
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/it-background-checks.html">
              Manufacturing / Technology
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
      <div class="sub-menu-content columns">
        <i class="icon-org-size"></i>
        <h3>
          By Organisational Size
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../solutions/global-background-checks.html">
              International (Multiple Countries)
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/enterprise-background-checks.html">
              Enterprise (7,000+)
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/company-background-checks.html">
              Mid-Market (501 - 7,000)
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/small-business-background-checks.html">
              Small Business (< 500)
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
      <div class="sub-menu-content columns">
        <i class="icon-service"></i>
        <h3>
          By Service
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../solutions/employment-background-checks.html">
              Background Checks
            </a>
            <div class="hr"></div>
          </li>
          
          <li>
            <a class="financial-services" href="../solutions/analytics-and-reporting.html">
              Analytics & Reporting
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
    </div>
  </div>
  
  <div class="columns menu-content integrations">
    <div class="row">
      <div class="columns">
        <ul>
          <li>
            <a class="overview" href="../integrations-and-apis.html">
            Integrations & APIs Overview
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row">
      
      <div class="sub-menu-content columns">
        <i class=""></i>
        <h3>
          
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="rest-api.html">
              REST API
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
    </div>
  </div>
  
  <div class="columns menu-content resources">
    <div class="row">
      <div class="columns">
        <ul>
          <li>
            <a class="overview" href="../resources.html">
            Resources Overview
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row">
      
      <div class="sub-menu-content columns">
        <i class=""></i>
        <h3>
          
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../resources.html">
              Thought Leadership
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
    </div>
  </div>
  
  <div class="columns menu-content why-fadv">
    <div class="row">
      <div class="columns">
        <ul>
          <li>
            <a class="overview" href="../why-first-advantage.html">
            Why First Advantage Overview
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row">
      
      <div class="sub-menu-content columns">
        <i class=""></i>
        <h3>
          
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../why-first-advantage/customer-promise.html">
              Our Customer Promise
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
    </div>
  </div>
  
  <div class="columns menu-content ">
    <div class="row">
      <div class="columns">
        <ul>
          <li>
            <a class="overview" href="../get-started.html">
            
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row">
      
      <div class="sub-menu-content columns">
        <i class=""></i>
        <h3>
          
        </h3>
        <ul class="menu-links">
          
          <li>
            <a class="financial-services" href="../get-started.html">
              Get Started
            </a>
            <div class="hr"></div>
          </li>
          
        </ul>
      </div>
      
    </div>
  </div>
  </div>
<!--END Mega Menu markup-->
<!--Global Mobile Nav Markup-->
<div class="mobile-menu show-for-medium-down">
  <div class="fadv-logo">
    <a href="../index.html">
      <img src="../Fadv-prod/media/Assets/Logos/Corporate/FADV-Logo-Background-Check-Services.png"/>
    </a>
  </div>
  
</div>

<div id="ctl00_plcMain_plcLeft_lt_zoneHead_Menu_dl_menu" class="dl-menuwrapper">
  
</div>
<!--END Global Mobile Nav Markup-->
                
            </div>
            <div>
                <div>
  <div class="small-hero" style="margin-top: 50px; height:50px; background-image:url('../Fadv-prod/media/Assets/Photos/Heroes/Small/APIs-REST-Background-Check-API51fb.jpg?ext=.jpg')">
    <div class="row" style="background-color:rgba(0,0,0,0.5)">
      <div class="small-12 medium-6 columns columns">
        <div class="hero-title" style="top:0px">
          
          <p>
            Dashboard
          </p>
          
                </div>
      </div>
      <div class="small-12 medium-6 columns">
        
      </div>
    </div>
  </div>
</div>

            </div>
            <div class="row bread hide-for-small">
               
                  <div class="container">
  <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" runat="server" href="#home">Data Entry Form</a></li>
<%--    <li><a data-toggle="tab" runat="server"  href="#menu2">Match Found</a></li>
    <li><a data-toggle="tab" runat="server" href="#menu3">Record</a></li>--%>
  </ul>
                    
     <div class="tab-content" style="background:#f2f2f2">
    <div id="home" class="tab-pane fade in active">
        <div class="small-12 columns">
            
          </div>
      <h3>Data Entry Form</h3>
        <div style="margin-right:0px;padding-right:1px; margin-left:800px ">
         Agent Name: <asp:Label ID="lblAgentName" runat="server"></asp:Label>
            </div>
        <table style="width:65%">
            <tr>
                
                <td> Fax ID: <asp:Label ID="lblFxId" runat="server"></asp:Label> <asp:Label ID="lblNotAvailaible" runat="server" Visible="false" Text="Not Availaible"></asp:Label> </td>

               
                <td style="width:45%;padding-left:60px" > Select DocType <asp:DropDownList ID="ddlDoctype" OnSelectedIndexChanged="ddlDoctype_SelectedIndexChanged" runat="server" AutoPostBack="true" Width="300"></asp:DropDownList> <asp:Label ID="lblNoTaskAssigned" runat="server" Visible="false" Text="No Task Assigned"></asp:Label></td>
            <td>  </td>
            </tr>
            <tr>
                <td>Select Customer<asp:DropDownList ID="ddlcustomer" runat="server"></asp:DropDownList></td>
                <td></td>
                <td>Select Location<asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>
            </tr>
            
        </table>
          <asp:HiddenField ID="hf" runat="server" />
       
       <asp:HiddenField ID="hfDllDocType" runat="server" />
        <asp:Label lbl="lblData" runat="server"></asp:Label>

              <asp:Repeater ID="Rept" runat="server" OnItemDataBound="Rept_ItemDataBound">
                  <HeaderTemplate>
                      <table style="width:65%">
                  </HeaderTemplate>
                  <ItemTemplate>
                      <tr>
                          <td id="tdTitle" runat="server" style="width:45%; align-content:50px">
                               <asp:HiddenField ID="hfFieldControlId" runat="server" Value='<%#Eval("DynamicControlID") %>' />
                 <asp:HiddenField ID="hfFieldId" runat="server" Value='<%#Eval("ControlName") %>' />
                 <asp:HiddenField ID="hfFieldType" runat="server"  Value='<%#Eval("ControlType") %>' />
                 <asp:HiddenField ID="hfFieldTitle" runat="server"  Value='<%#Eval("labelName") %>' />
                              
                            
                               </td>
                          <td>  <asp:Label ID="lblMatchFound" runat="server" Visible="false"></asp:Label></td>
                         
                        <%--  <td>  <asp:Label ID="lblRequiredField" runat="server" Visible="false"></asp:Label></td>--%>
                          <td id="tdField" runat="server">

                          </td>
                              </tr>
                  </ItemTemplate>
                  <FooterTemplate>
                      </table>
                  </FooterTemplate>
              </asp:Repeater>
                  </tr>
               </table>   
         <asp:Button ID="btnNext" class="mq-btn" runat="server" OnClick="btnNext_Click" Visible="false"  Text="Next" />
     <asp:Button ID="Button2" class="mq-btn" runat="server" OnClick="Button2_Click" Visible="false"  Text="Submit" />
    </div>
    <div id="menu12" class="tab-pane fade">
         <asp:Button ID="Button1" class="mq-btn" runat="server" data-toggle="pill" data-target="#home" Text="Back TO Work Sheet"/>
       
      <h3>Data Entry</h3>
         <div class="small-12 medium-6 columns">
             <table>
                 <tr>
                     <td style="width:300px">Agent ID: <asp:Label ID="lblAgentId" class="text" runat="server" Text=""></asp:Label></td>
                    <td style="width:300px"><label>Fax ID:</label> <asp:TextBox ID="txtfaxID" runat="server" Width="100px"></asp:TextBox>
            </td>             
                     <td>Client Name: <asp:Label ID="Label8" class="text" runat="server" Text="Not Clear"></asp:Label></td>
                 </tr>
             </table><br />

            
         <asp:Label ID="Label1" class="text" runat="server" Text="Verification ID"></asp:Label>
        <asp:TextBox ID="txtVerification" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
         <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label2" class="text" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
         <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label3" class="text" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
         <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label4" class="text" runat="server" Text="Phone"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
        <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label5" class="text" runat="server" Text="State"></asp:Label>
        <asp:TextBox ID="txtState" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
         <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label6" class="text" runat="server" Text="Country"></asp:Label>
        <asp:TextBox ID="txtCountry" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
         <div class="small-12 medium-6 columns">
            
         <asp:Label ID="Label7" class="text" runat="server" Text="Address"></asp:Label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="small-12 medium-6 columns"></asp:TextBox>
            
            <small>
              Please Enter a User Name
            </small>
          </div>
        <div class="small-12 columns">
        <asp:Button ID="btnLogin" class="mq-btn"  runat="server" Text="Submit"/>
            
          </div>

      </div>
    <div id="menu2" class="tab-pane fade">
      <h3>Menu 2</h3>
      <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam.</p>
    </div>
    <div id="menu3" class="tab-pane fade">
      <h3>Menu 3</h3>
      <p>Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
    </div>
  </div>
  
 </div>
                
            </div>
            <div>
                <div>
  
</div>

            </div>
            <div style="width: 100%;">
                <div>
  
</div>

            </div>
            <div>
                


<!--OptIn Markeup-->


<!--END OptIn Markup-->
            </div>
            <div>
               


<!--Global Footer Markup-->

<!--END Global Footer Markup-->
<!-- below is the dialog window that shows all the images -->
<div class="dialog dialog-for-images">
  <div class="dialog-overlay">
    <div class="dialog-x-close">
      <i class="icon-close"></i>
    </div>
  </div>
  <div class="dialog-content">
    <img src="#"/>
    
    <div class="dialog-count dialog-hidden">
    </div>
    <div class="dialog-previous dialog-hidden">
    </div>
    <div class="dialog-next dialog-hidden">
    </div>
  </div>
</div>
<!-- above is the dialog window that shows all the images -->
                
            </div>
        

            <!-- /main content -->
        </div>
        <!-- footer -->    
        
    </div>
    


</form>
    <!-- below is the dialog window that shows all the images -->
    <div class="dialog dialog-for-images">
        <div class="dialog-overlay">
            <div class="dialog-x-close"><i class="icon-close"></i></div>
        </div>
        <div class="dialog-content">

            <img src="#">

            <div class="dialog-count dialog-hidden"></div>
            <div class="dialog-previous dialog-hidden"><</div>
            <div class="dialog-next dialog-hidden">></div>

        </div>
    </div>
    <!-- above is the dialog window that shows all the images -->
    <!--contact flyout-->
    <div class="contact">
        <div class="flyout">
            <div class="cta columns">
                <span>Contact</span>
            </div>
            <div class="phone columns">
                <a href="tel:8447180087">
                    <i class="icon-customer-service"></i>
                </a>
                <a href="tel:8447180087">
                    <span>844.718.0087</span>
                </a>
            </div>
            <div class="email columns">
                <a href="mailto:solutions@fadv.com">
                    <i class="icon-email"></i>
                </a>
                <a href="mailto:solutions@fadv.com">
                    <span>Email</span>
                </a>
            </div>
        </div>
    </div>
            
    <!--Back to top-->
    <a class="btt hide-for-small">
        <div>
            <i class="icon-arrow-up"></i>
            <span>Top</span>
        </div>
    </a>
   
</body>
</html>
