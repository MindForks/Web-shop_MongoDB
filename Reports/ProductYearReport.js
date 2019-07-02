db.product.aggregate([
    {
        $lookup:
        {
            from: "order",
            localField: "_id",
            foreignField: "productIDs",
            as: "order"
        }
    },
    { 
        $project: 
        {
            "_id": "$_id",
            "Title": "$Title",
            "CountInStock": "$CountInStock",
            "Price": "$Price",
            "Manufacturer": "$Manufacturer",
            "Jan" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 1] },
                    }
                }
            },
            "Feb" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 2] },
                    }
                }
            },
            "Mar" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 3] },
                    }
                }
            },
            "Apr" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 4] },
                    }
                }
            },
            "May" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 5] },
                    }
                }
            },
            "June" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 6] },
                    }
                }
            },
            "July" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 7] },
                    }
                }
            },
            "Aug" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 8] },
                    }
                }
            },
            "Sept" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 9] },
                    }
                }
            },
            "Oct" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 10] },
                    }
                }
            },
            "Nov" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 11] },
                    }
                }
            },
            "Dec" : {
                $size: {
                    $filter: {
                        input: "$order",
                        as: "tmp",
                        cond: { $eq: [{ $month: "$$tmp.CreationDate" }, 12] },
                    }
                }
            },
        }
    },
]);