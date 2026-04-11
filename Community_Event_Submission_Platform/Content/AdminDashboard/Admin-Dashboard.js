window.addEventListener('load', function () {

    // Destroy existing chart instance if present (important on page refresh)
    var existingChart = Chart.getChart('combinedChart');
    if (existingChart) {
        existingChart.destroy();
    }

    var ctx = document.getElementById('combinedChart').getContext('2d');

    // chartLabels / chartUsersData / chartEventsData are injected by Dashboard.cshtml via @Html.Raw
    new Chart(ctx, {
        data: {
            labels: chartLabels,
            datasets: [
                {
                    type: 'line',
                    label: 'Total Users',
                    data: chartUsersData,
                    borderColor: '#6366f1',
                    backgroundColor: 'rgba(99,102,241,0.12)',
                    fill: true,
                    tension: 0.4,
                    pointRadius: 5,
                    pointBackgroundColor: '#6366f1',
                    borderWidth: 2.5,
                    yAxisID: 'yUsers'
                },
                {
                    type: 'bar',
                    label: 'All Events',
                    data: chartEventsData,
                    backgroundColor: 'rgba(16,185,129,0.55)',
                    hoverBackgroundColor: 'rgba(16,185,129,0.8)',
                    borderRadius: 5,
                    borderSkipped: false,
                    yAxisID: 'yEvents'
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: {
                duration: 2000,
                easing: 'easeInOutQuart',
                delay: function (context) {
                    var delay = 0;
                    if (context.type === 'data' && context.mode === 'default') {
                        delay = context.dataIndex * 100;
                    }
                    return delay;
                }
            },
            animations: {
                tension: {
                    duration: 1000,
                    easing: 'linear',
                    from: 1,
                    to: 0.4,
                    loop: false
                },
                y: {
                    duration: 1500,
                    from: 0
                }
            },
            interaction: {
                mode: 'index',
                intersect: false
            },
            plugins: {
                legend: { display: false },
                tooltip: {
                    backgroundColor: '#1e2538',
                    titleColor: '#fff',
                    bodyColor: '#9ca3af',
                    padding: 10,
                    cornerRadius: 8,
                    animation: { duration: 400 }
                }
            },
            scales: {
                x: {
                    grid: { color: 'rgba(255,255,255,0.04)' },
                    ticks: { color: '#6b7280', font: { size: 11 } }
                },
                yUsers: {
                    type: 'linear',
                    position: 'left',
                    grid: { color: 'rgba(255,255,255,0.04)' },
                    ticks: { color: '#818cf8', font: { size: 11 } },
                    title: {
                        display: true,
                        text: 'Users',
                        color: '#818cf8',
                        font: { size: 11 }
                    }
                },
                yEvents: {
                    type: 'linear',
                    position: 'right',
                    grid: { drawOnChartArea: false },
                    ticks: {
                        color: '#34d399',
                        font: { size: 11 },
                        stepSize: 2
                    },
                    title: {
                        display: true,
                        text: 'Events',
                        color: '#34d399',
                        font: { size: 11 }
                    }
                }
            }
        }
    });

});

// Force re-animation when page is restored from bfcache (back/forward button)
window.addEventListener('pageshow', function (event) {
    if (event.persisted) {
        location.reload();
    }
});