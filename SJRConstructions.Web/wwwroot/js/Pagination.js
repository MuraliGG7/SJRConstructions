let pageSize = 10; // Number of rows per page
let currentPage = 1; // Current page
let totalPages = 0;
function createPagination(totalPages) {
    let pagination = $('#pagination').empty();
    let visiblePages = 5; // Number of visible page links
    let startPage = 1;
    let endPage = Math.min(totalPages, startPage + visiblePages - 1);

    if (currentPage > Math.floor(visiblePages / 2)) {
        startPage = currentPage - Math.floor(visiblePages / 2);
        endPage = currentPage + Math.floor(visiblePages / 2);
    }

    if (endPage - startPage < visiblePages - 1) {
        startPage = endPage - visiblePages + 1;
    }



    if (totalPages < endPage) {
        endPage = totalPages;
        startPage = endPage - Math.floor(visiblePages - 1);
    }
    if (startPage < 1) {
        startPage = 1;
    }
    if (totalPages != 0 && totalPages != 1) {
        for (let i = startPage; i <= endPage; i++) {
            let li = $('<li class="page-item"><a class="page-link" href="#">' + i + '</a></li>');
            if (i === currentPage) {
                li.addClass('active');
            }
            pagination.append(li);
        }
    }
    updatePaginationButtons(currentPage, totalPages);
}
function updatePaginationButtons(currentPage, totalPages) {
    if (currentPage === 1) {
        $('#prevBtn').hide();
    } else {
        $('#prevBtn').show();
    }
    if (currentPage === totalPages || totalPages == 0) {
        $('#nextBtn').hide();
    } else {
        $('#nextBtn').show();
    }
}
$(document).ready(function () {
    $('#pagination').on('click', 'li.page-item', function () {
        currentPage = parseInt($(this).text());
        populateTable(currentPage);
        createPagination(totalPages);
    });
    $('#prevBtn').click(function (event) {
        event.preventDefault();
        if (currentPage > 1) {
            currentPage--;
            populateTable(currentPage);
            createPagination(totalPages);

        }
    });

    // Next button click event
    $('#nextBtn').click(function (event) {
        event.preventDefault();
        if (currentPage < totalPages) {
            currentPage++;
            populateTable(currentPage);
            createPagination(totalPages);
        }
    });
});