using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DAL
{
    public interface IWonder
    {
        #region GetAllProducts
        List<ProcessorVM> GetAllProcessors(int userid = 0, string deleteddata = null);
        List<MotherboardVM> GetAllMotherboard(int userid = 0, string deleteddata = null);
        List<HddVM> GetAllHDD(int userid = 0, string deleteddata = null);
        List<RamVM> GetAllRAM(int userid = 0, string deleteddata = null);
        List<SsdVM> GetAllSSD(int userid = 0, string deleteddata = null);
        List<GraphicsCardVM> GetAllCard(int userid = 0, string deleteddata = null);
        List<CaseVM> GetAllCase(int userid = 0, string deleteddata = null);
        List<PowerSupplyVM> GetAllPowerSuply(int userid = 0, string deleteddata = null);

        #endregion

        #region StorePage
        #region Processor

        IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers();

        IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id);

        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName,  int id, int min=100, int max= 50000, int userid = 0);

        IEnumerable<ProcessorVM> ProcessorPrice(int min=100, int max= 50000, int userid = 0);
        IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id, int userid = 0);

        IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand( int sort, int min = 100, int max = 50000, int userid = 0);

    
        #endregion

        #region Motherboard
        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers(int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVM, int Id, int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0);
        IEnumerable<MotherboardVM> MotherboardPrice(int min=100, int max=50000,  int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id, int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);


        #endregion

        #region HDD
        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers(int userid = 0);
        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVM, int Id, int userid = 0);
        IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int id, int min = 100, int max = 50000, int userid = 0);
        IEnumerable<HddVM> HDDPrice(int min = 100, int max = 50000, int userid = 0);
        IEnumerable<HddVM> GetHDDDependentOnSort(int id, int userid = 0);
        IEnumerable<HddVM> GetHDDPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);
        #endregion

        #region RAM


        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers(int userid = 0);

        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id, int userid = 0);

        IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0);

        IEnumerable<RamVM> RAMPrice(int min=100, int max=50000, int userid = 0);
      
        IEnumerable<RamVM> GetRAMDependentOnSort(int id,int userid = 0);

        IEnumerable<RamVM> GetRAMPriceDependentOnBrand( int sort, int min=100, int max=500000, int userid = 0);

        #endregion

        #region SSd

        IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers();

        IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> hddVM, int Id);

        IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<SsdVM> SSDPrice(int min, int max, int PSize, int NPage);
        IEnumerable<SsdVM> SSDPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<SsdVM> SSDPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<SsdVM> GetSSDDependentOnSort(int id);
        IEnumerable<SsdVM> GetSSDPriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region Graphics Card

        IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCardBrandNamesAndNumbers();

        IEnumerable<GraphicsCardVM> GetCardProductsByPrice(IEnumerable<GraphicsCardVM> hddVM, int Id);

        IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<GraphicsCardVM> CardPrice(int min, int max, int PSize, int NPage);
        IEnumerable<GraphicsCardVM> CardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<GraphicsCardVM> CardPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<GraphicsCardVM> GetCardDependentOnSort(int id);
        IEnumerable<GraphicsCardVM> GetCardPriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region Case

        IEnumerable<CaseVM> CasePaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCaseBrandNamesAndNumbers();

        IEnumerable<CaseVM> GetCaseProductsByPrice(IEnumerable<CaseVM> hddVM, int Id);

        IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<CaseVM> CasePrice(int min, int max, int PSize, int NPage);
        IEnumerable<CaseVM> CasePriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<CaseVM> CasePaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<CaseVM> GetCaseDependentOnSort(int id);
        IEnumerable<CaseVM> GetCasePriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region PowerSuply

        IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetPowerSuplyBrandNamesAndNumbers();

        IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByPrice(IEnumerable<PowerSupplyVM> PSVM, int Id);

        IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<PowerSupplyVM> PowerSuplyPrice(int min, int max, int PSize, int NPage);
        IEnumerable<PowerSupplyVM> PowerSuplyPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<PowerSupplyVM> PowerSuplyPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<PowerSupplyVM> GetPowerSuplyDependentOnSort(int id);
        IEnumerable<PowerSupplyVM> GetPowerSuplyPriceDependentOnBrand(int min, int max, int sort);
        #endregion
        #endregion

        #region Product Details

        CaseVM CaseDetails(string code);

        GraphicsCardVM GraphicsCardDetails(string code);

        HddVM HddDetails(string code);

        MotherboardVM MotherboardDetails(string code);

        PowerSupplyVM PowerSupplyDetails(string code);

        ProcessorVM ProcessorDetails(string code);

        RamVM RamDetails(string code);

        SsdVM SsdDetails(string code);

        #endregion

        #region comments Pagination 
        CaseVM CaseCommentsPagination(string code, int currentPageIndex);

        #endregion

        #region NewProducts
        List<MotherboardVM> GetNewMotherBoards(int userid = 0);

        List<ProcessorVM> GetNewProcessors(int userid = 0);

        List<RamVM> GetNewRam(int userid = 0);

        List<GraphicsCardVM> GetNewVGA(int userid = 0);

        List<HddVM> GetNewHDD(int userid = 0);

        List<SsdVM> GetNewSSD(int userid = 0);

        List<PowerSupplyVM> GetNewPSU(int userid = 0);

        List<CaseVM> GetNewCase(int userid = 0);

        #endregion

        #region TopSelling

        public List<CaseVM> GetTopCases(int userid = 0);
        public List<GraphicsCardVM> GetTopVgas(int userid = 0);
        public List<HddVM> GetTopHdds(int userid = 0);
        public List<MotherboardVM> GetTopMotherboards(int userid = 0);
        public List<PowerSupplyVM> GetTopPsus(int userid = 0);
        public List<ProcessorVM> GetTopProcessors(int userid = 0);
        public List<RamVM> GetTopRams(int userid = 0);
        public List<SsdVM> GetTopSsds(int userid = 0);


        #endregion

        #region Check Order
        string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData);

        string CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData);

        string CheckOrder(SalesVM[] OrderData);

        #endregion

        #region WishList

        List<WishListVM> GetWishList(int userid);
        string DeletefromWL(string ProductCode, int userid);
        string CheckfromWL(string ProductCode, int userid);
        string AddToWL(string ProductCode, int userid);

        #endregion

        #region Review
        public ReviewVM AddReview(ReviewVM review);
        public List<ReviewVM> Reviews(string code);
        #endregion

        #region Search
        public List<Search> SearchProduct(string src);
        public List<Search> SearchMotherBoard(string src);
        public List<Search> SearchProcessor(string src);
        public List<Search> SearchRam(string src);
        public List<Search> SearchSSD(string src);
        public List<Search> SearchHDD(string src);
        public List<Search> SearchCase(string src);
        public List<Search> SearchPowerSupply(string src);
        public List<Search> SearchVGA(string src);
        public List<Search> SearchFunction(string src, int num);
        #endregion

        public List<Brand> GetProductBrand();

        #region Admin Project

        public List<UserVM> GetUsersData();

        public List<UserVM> GetAdmins();

        public List<SalesVM> GetProcessor();
        public List<SalesVM> GetMotherboard();
        public List<SalesVM> GetSDD();
        public List<SalesVM> GetHDD();
        public List<SalesVM> GetCases();
        public List<SalesVM> GetPowerSupplies();
        public List<SalesVM> GetGraphicsCard();
        public List<SalesVM> GetRam();

        #endregion

        public List<ChatVM> GetAllMessages(int userid);
        
    }
}