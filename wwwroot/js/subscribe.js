
document.querySelector("#subscribe-btn").addEventListener("click", function () {
    var formData = new FormData();
    
    formData.append("email", document.querySelector("#mce-EMAIL").value);
    axios.post('/ajax/subscribe', formData)
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
    if (document.querySelector("#mce-EMAIL").value == "") {
        document.querySelector("#error").style.display = "block";
        
    }
    else {
        document.querySelector("#error").style.display = "none";
        document.querySelector("#tempdata").style.display="block"
        
    }
    function HideTempdata() {
        document.querySelector("#tempdata").style.display = "none";

    }
    setTimeout(HideTempdata, 4000);
    document.querySelector("#mce-EMAIL").value = "";
    
   
});
