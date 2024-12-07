// const openNavMenu = document.querySelector(".mobile-menu");
// const mobileLinks = document.querySelector(".mobile-link")
// const closeNavMenu = document.querySelector(".close-menu")

// function openNavMenuHandle() {
//     mobileLinks.style.right = "0";
//     mobileLinks.style.width = "100%";
// }

// openNavMenu.addEventListener("click", openNavMenuHandle);


// function closeMenuHandle() {
//     mobileLinks.style.right = "-800px";
//     mobileLinks.style.width = "70%";
// }
// closeNavMenu.addEventListener("click", closeMenuHandle);

document.addEventListener("DOMContentLoaded", function() {
    var acc = document.getElementsByClassName("accordion-button");
    for (var i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function() {
            this.classList.toggle("active");
            var panel = this.nextElementSibling;
            if (panel.style.display === "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        });
    }

    var accGroups = document.getElementsByClassName("accordion-button-groups");
    for (var i = 0; i < accGroups.length; i++) {
        accGroups[i].addEventListener("click", function() {
            this.classList.toggle("active");
            var panelGroups = this.nextElementSibling;
            if (panelGroups.style.display === "block") {
                panelGroups.style.display = "none";
            } else {
                panelGroups.style.display = "block";
            }
        });
    }

    var accTasks = document.getElementsByClassName("accordion-button-tasks");
    for (var i = 0; i < accTasks.length; i++) {
        accTasks[i].addEventListener("click", function() {
            this.classList.toggle("active");
            var panelTasks = this.nextElementSibling;
            if (panelTasks.style.display === "block") {
                panelTasks.style.display = "none";
            } else {
                panelTasks.style.display = "block";
            }
        });
    }
});