//redirectToCheckout = function (sessionId) {
//    var stripe = Stripe('pk_test_51JU9AmK1RA8azwxih50QLUw9f3YGNt7QzdccdIKER3io1qRYgsX48b4jruLDOX8IFSivpAwWrdQ718aBHp6EYDUT00epKYUKip');
//    stripe.redirectToCheckout({
//        sessionId: sessionId
//    });
//};

function redirectToCheckout(sessionId) {
    var stripe = Stripe('pk_test_51JU9AmK1RA8azwxih50QLUw9f3YGNt7QzdccdIKER3io1qRYgsX48b4jruLDOX8IFSivpAwWrdQ718aBHp6EYDUT00epKYUKip');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
}
