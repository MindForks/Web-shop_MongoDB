const prodTitle = "Monitor";
db.order.aggregate([
   {
      $lookup:
       {
        from: "product",
        localField: "productIDs",
        foreignField: "_id",
        as: "productIDs"
      },
  
   },
   { 
      $match: {"productIDs.Title": prodTitle}
   },
  {
      $project: {
        UserName: "$UserName"
      }
   }
])