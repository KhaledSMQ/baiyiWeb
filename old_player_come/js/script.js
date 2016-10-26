 var NavCtl = {};
      NavCtl =
          {
    mOver: function () {
			document.getElementById("topnav_catalog_ct").style.display = "block";
			document.getElementById("topnav_catalog_box").onmouseout = this.mOut;
			document.getElementById("topnav_catalog_box").className = "catalog_box2"; 
			},
    mOut: function () {
		document.getElementById("topnav_catalog_ct").style.display = "none"; 
		document.getElementById("topnav_catalog_box").className = "catalog_box"; 
		},
    liOver: function (t) { 
		var divs = t.getElementsByTagName("div"); 
		for (var i = 0; i < divs.length; i++) { 
			divs[i].style.display = "block"; 
			} 
			},
    liOut: function (t) {
	var divs = t.getElementsByTagName("div"); 
	for (var i = 0; i < divs.length; i++) { 
	divs[i].style.display = "none"; 
	} 
	}
    
       }