﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PyramidBinaryTreeCalculator.Services.PyramidBinaryTreeCalculateService;
using PyramidBinaryTreeCalculator.Services.PyramidBinaryTreeReadService;

namespace PyramidBinaryTreeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            var pyramidBinaryTreeReadService = serviceProvider.GetService<IPyramidBinaryTreeReadService>();
            var pyramidBinaryTreeCalculateService = serviceProvider.GetService<IPyramidBinaryTreeCalculateService>();

            try
            {
                const string unparsedPyramidBinaryTree = @"215
                    192 124
                    117 269 442
                    218 836 347 235
                    320 805 522 417 345
                    229 601 728 835 133 124
                    248 202 277 433 207 263 257
                    359 464 504 528 516 716 871 182
                    461 441 426 656 863 560 380 171 923
                    381 348 573 533 448 632 387 176 975 449
                    223 711 445 645 245 543 931 532 937 541 444
                    330 131 333 928 376 733 017 778 839 168 197 197
                    131 171 522 137 217 224 291 413 528 520 227 229 928
                    223 626 034 683 839 052 627 310 713 999 629 817 410 121
                    924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

                Console.WriteLine("Starting pyramid binary tree read application...");
                Console.WriteLine();

                var pyramidBinaryTree = pyramidBinaryTreeReadService.ReadAndMapPyramidBinaryTree(unparsedPyramidBinaryTree);
                var maximumSumResults = pyramidBinaryTreeCalculateService.CalculatePyramidBinaryTreeMaximumSumResults(pyramidBinaryTree);

                if (maximumSumResults == null)
                {
                    Console.WriteLine($"Maximum sum of pyramid binary tree path was not found.");
                }
                else
                {
                    Console.WriteLine($"Maximum sum of pyramid binary tree path is {maximumSumResults.PathSum}.");
                    Console.WriteLine($"Pyramid binary tree path is: {string.Join(", ", maximumSumResults.Path)}.");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddSingleton<IPyramidBinaryTreeReadService, PyramidBinaryTreeReadService>()
                .AddSingleton<IPyramidBinaryTreeCalculateService, PyramidBinaryTreeCalculateService>()
                .BuildServiceProvider();
        }
    }
}
