$(document).ready(() => {
$(document).on('click', '.btnChildDelete', function (e) {
    e.preventDefault();
    const dataTarget = $(this).parent().parent().find('button');
    const element = $(dataTarget[0]).parent().parent().parent();
    const children = [];
    Array.from(dataTarget).forEach(target => {
        
        const targetId = $(target).attr('data-bs-target');
        targetId !== undefined
            ? children.push(targetId.substr(6))
            : null
        
    })

    $.ajax({
        type: 'DELETE',
        url: "/Home/DeleteChild",
        data: { id: 0, ids: children },
        beforeSend: function () {
            $.when(element.fadeOut())
                .then(() => {
                    element.remove();
                })
        },
        success: function (response) {
            //console.log(response)
        },
        error: function (err) {
            console.log(err);
        }
    })
})
})
    
