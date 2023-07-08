$(document).ready(() => {
    $(document).on('click', '.btnMainDelete', function (e) {
        e.preventDefault();
        const dataTarget = $(this).parent().parent().find('button');
        const element = $(dataTarget[0]).parent().parent();
        const children = [];
        //console.log(dataTarget)
        Array.from(dataTarget).forEach(target => {
            const targetId = $(target).attr('data-bs-target');
            if (targetId) {
                if (targetId.includes('main')) {
                    targetId !== undefined
                    ? children.push(targetId.substr(5))
                    : null
            }
                else {
                    targetId !== undefined
                    ? children.push(targetId.substr(6))
                    : null
            }
            }
            
            

        })

        $.ajax({
            type: 'DELETE',
            url: '/Home/DeleteMain',
            data: { id: 0, ids: children },
            beforeSend:function() {
                $.when(element.fadeOut())
                    .then(() => {
                        element.remove();
                    })
            },
            success: function (response) {
                console.log(response)
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
})