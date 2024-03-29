using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> FunctionHandler(string input, ILambdaContext context)
        {
            var reckognitionClient = new AmazonRekognitionClient();
            var detectResponse = await reckognitionClient.DetectLabelsAsync(
                 new DetectLabelsRequest
                 {
                     Image = new Image
                     {
                         S3Object = new S3Object
                         {
                             Bucket = "rekogniton1907154",
                             Name = input

                         }
                     }
                 }
                 );

            foreach(var label in detectResponse.Labels)
            {
                if(label.Confidence > 50)
                {
                    if(label.Name=="Fried Chicken" || label.Name == "Nuggets")
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }
    }
} 
