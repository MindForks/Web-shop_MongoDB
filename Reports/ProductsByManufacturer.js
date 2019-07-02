db.product.aggregate([
  {
    $group: {
        _id: "$Manufacturer.Title",
        total: {
            $sum: 1
        },
        products: {
            $push: "$Title"
        }
    }
}
]);