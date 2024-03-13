// Define a function to toggle the sidebar's visibility
function toggleSidebar() {
    // Use document.querySelector to find the first element with the class 'sidebar'
    var sidebar = document.querySelector('.sidebar');
    // Find the first element with the class 'overlay'
    var overlay = document.querySelector('.overlay');

    // Toggle the class 'sidebar-expanded' on the sidebar.
    // If 'sidebar-expanded' is present, it gets removed; if it's absent, it gets added.
    sidebar.classList.toggle('sidebar-expanded');

    // Set the display style of the overlay based on whether 'sidebar-expanded' is present
    // If 'sidebar-expanded' is present, set the overlay to 'block', making it visible.
    // Otherwise, set it to 'none', hiding it.
    overlay.style.display = sidebar.classList.contains('sidebar-expanded') ? 'block' : 'none';
}

// Define a function to close the sidebar
function closeSidebar() {
    // Again, find the sidebar and overlay elements in the document
    var sidebar = document.querySelector('.sidebar');
    var overlay = document.querySelector('.overlay');

    // Remove the class 'sidebar-expanded' from the sidebar, ensuring it's hidden
    sidebar.classList.remove('sidebar-expanded');

    // Set the overlay display to 'none', hiding it
    overlay.style.display = 'none';
}
