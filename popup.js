// Pop up quang cao
window.addEventListener("load", function(){
  setTimeout(()=>{
    document.getElementById("popup").classList.add("show");
  },2000);
});

document.getElementById("close-popup").onclick = function(){
  document.getElementById("popup").classList.remove("show");
};